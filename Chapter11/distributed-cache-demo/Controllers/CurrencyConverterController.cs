using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace distributed_cache_demo.Controllers
{
    [Route("api/[controller]")]
    public class CurrencyConverterController : Controller
    {
        private readonly IDistributedCache _cache;

        public CurrencyConverterController(IDistributedCache cache)
        {
            _cache = cache;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rate = await GetExchangeRatesFromCache();
            if (rate != null)
            {
                return Ok(rate);
            }

            await SetExchangeRatesCache();
            return Ok(await GetExchangeRatesFromCache());
        }       

        private async Task SetExchangeRatesCache()
        {
            var ratesObj = await DownloadCurrentRates();
            byte[] ratesObjval = Encoding.UTF8.GetBytes(ratesObj);

            await _cache.SetAsync("ExchangeRates", ratesObjval, new DistributedCacheEntryOptions()                    
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60))                    
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(240))
                );
        }

        private async Task<RatesRoot> GetExchangeRatesFromCache()
        {
            var rate = await _cache.GetAsync("ExchangeRates");
            if (rate != null)
            {
                var ratestr = Encoding.UTF8.GetString(rate);
                var ratesobj = JsonConvert.DeserializeObject<RatesRoot>(ratestr);
                return ratesobj;
            }
            return null;
        }


        private async Task<string> DownloadCurrentRates()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("http://api.fixer.io/latest?base=USD&symbols=USD,GBP,CAD,INR");
            var model = await response.Content.ReadAsStringAsync();            
            return model;
        }
    }
}
