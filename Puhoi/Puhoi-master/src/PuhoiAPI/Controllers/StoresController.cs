using System;
using Microsoft.AspNetCore.Mvc;
using Puhoi.Business.Interfaces;
using Puhoi.Models.Models;
using Puhoi.Models;
using System.Net;
using PuhoiAPI.Attributes;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class StoresController : Controller
    {
        private readonly IStoreManager _storeManager;

        public StoresController(IStoreManager storeManager)
        {
            _storeManager = storeManager;
        }

        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            HttpModelResult modelResult = _storeManager.Get(id);
            if (modelResult.HttpStatus == HttpStatusCode.OK)
            {
                StoreModel storeModel = modelResult.Model as StoreModel;
                return Ok(storeModel);
            }

            switch (modelResult.HttpStatus)
            {
                case HttpStatusCode.NotFound:
                    return NotFound();
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            HttpModelResult modelResult = _storeManager.GetAll();
            return Ok(modelResult.Models);
        }

        [Route("healthcheck")]
        [HttpGet]
        public IActionResult HealthCheckGet()
        {
            return Ok();
        }

        [Route("")]
        [HttpPost]
        [ModelValidation]
        public IActionResult Post([FromBody] StoreModel model)
        {
            HttpModelResult modelResult = _storeManager.Add(model);
            if (modelResult.HttpStatus == HttpStatusCode.Created)
            {
                return new CreatedResult(
                    string.Format("/api/stores/{0}",
                    modelResult.Model.Id),
                    modelResult.Model);
            }
            return new StatusCodeResult((int)modelResult.HttpStatus);
        }
        
        // PUT api/values/5

        [HttpPut("{id}")]
        [ModelValidation]
        public IActionResult Put(Guid id, [FromBody]StoreModel model)
        {
            HttpModelResult modelResult = _storeManager.Update(model,id);
            if (modelResult.HttpStatus == HttpStatusCode.Created)
            {
                return new CreatedResult(
                    string.Format("/api/stores/{0}",
                    modelResult.Model.Id),
                    modelResult.Model);
            }
            return new StatusCodeResult((int)modelResult.HttpStatus);
        }


        // DELETE api/values/5

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            HttpModelResult modelResult = _storeManager.Delete(id);
            return new StatusCodeResult((int) modelResult.HttpStatus);
        }
    }
}
