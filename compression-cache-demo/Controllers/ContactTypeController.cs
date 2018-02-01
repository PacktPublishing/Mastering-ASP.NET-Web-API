using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace compression_cache_demo.Controllers
{
    [Route("api/[controller]")]
    public class ContactTypeController : Controller
    {
        private string connectionString;
        private readonly IMemoryCache _cache;       

        public ContactTypeController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=AdventureWorks2014;Integrated Security=True";
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Look for cache key.
            if (!_cache.TryGetValue("ContentTypeKey", out IList<ContactType> contentList))
            {
                // Setting the cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                contentList = await GetAllContactTypesAsync();
                // Save data in cache.
                _cache.Set("ContentTypeKey", contentList, cacheEntryOptions);
            }
            return Ok(contentList);            
        }

        /// <summary>
        /// Gets All ContactType from DB or Cache        
        /// </summary>
        private async Task<IList<ContactType>> GetAllContactTypesAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var records = await dbConnection.QueryAsync<ContactType>("SELECT ContactTypeID, Name FROM Person.ContactType");
                return records.ToList();
            }
        }
  

        /// <summary>
        /// Gets All ContactType from DB 
        /// Used for Application performance
        /// </summary>
        /// 
        [Route("GetAllContactTypes")]
        [HttpGet]
        public IList<ContactType> GetAllContactTypes()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var records = dbConnection.Query<ContactType>("spGetContactType", commandType:CommandType.StoredProcedure);
                return records.ToList();
            }
        }


        /// <summary>
        /// Gets All ContactType from DB or Cache in Async way
        /// Used for Application performance
        /// </summary>
        /// 
        [Route("GetAllContactTypeAsync")]
        [HttpGet]
        public async Task<IList<ContactType>> GetAllContactTypeAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var records = await dbConnection.QueryAsync<ContactType>("spGetContactType", commandType: CommandType.StoredProcedure);
                return records.ToList();
            }
        }
    }

    public class ContactType
    {
        public int ContactTypeID { get; set; }
        public string Name { get; set; }
    }
}