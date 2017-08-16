using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Puhoi.Models.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuhoiAPI.Tests
{
    [TestClass]
    public class StoresControllerShould 
    {
        public StoreModel GreenStoreModel()
        {
            return new StoreModel
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Green Model",
                NumberOfProducts = 5,
                DisplayName = "API "
            };
        }

        public StoreModel RedStoreModel()
        {
            return new StoreModel
            {
                Id = Guid.Empty,
                Name = "RedTest",
                Description = "Red Model",
                NumberOfProducts = 100,
                DisplayName = "API"
            };
        }

        private string _baseUri;
        private const string _configurationBaseUri = "baseUri";

        [TestInitialize]
        public void TestInit()
        {
            _baseUri = ConfigurationManager.AppSettings.Get(_configurationBaseUri);
        }

        [TestMethod]
        public async Task ReturnOkForHealthCheck()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                //act 
                HttpResponseMessage response = await client.GetAsync("stores/healthcheck");

                // assert
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }


        [TestMethod]
        public async Task ReturnCreatedForAPost()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                //act 
                StoreModel storeModel = GreenStoreModel();
                HttpResponseMessage response = await client.PostAsJsonAsync("stores", storeModel);
                // assert
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.Created);

                // clean up
                await DeleteStore(client, storeModel.Id);
            }
        }

        [TestMethod]
        public async Task ReturnConflictForAPost()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                //act 
                StoreModel storeModel = GreenStoreModel();
                storeModel.Name = "ConflictStore";
                HttpResponseMessage response = await client.PostAsJsonAsync("stores", storeModel);
                response = await client.PostAsJsonAsync("stores", storeModel);

                // act 
                response = await client.PostAsJsonAsync("stores", storeModel);

                // assert
                response.StatusCode.Should().Be(HttpStatusCode.Conflict);

                // clean up
                await DeleteStore(client, storeModel.Id);
            }
        }

        [TestMethod]
        public async Task ReturnOkForAPut()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                //act 
                StoreModel storeModel = GreenStoreModel();
                storeModel.Id = Guid.NewGuid();
                HttpResponseMessage response = await client.PostAsJsonAsync("stores", storeModel);
                storeModel.Name = "Creamy Cheese";
                // act 
                string putUri = string.Format("stores/{0}", storeModel.Id);
                response = await client.PutAsJsonAsync(putUri, storeModel);

                // assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                // clean up
                await DeleteStore(client, storeModel.Id);
            }
        }


        [TestMethod]
        public async Task ReturnOkForDelete()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                //act 
                StoreModel storeModel = GreenStoreModel();
                storeModel.Id = Guid.NewGuid();
                HttpResponseMessage response = await client.PostAsJsonAsync("stores", storeModel);
                // act 
                response = await DeleteStore(client, storeModel.Id);

                // assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        private async Task<HttpResponseMessage> DeleteStore( HttpClient client, Guid Id )
        {
            string deleteUri = string.Format("stores/{0}", Id);
            HttpResponseMessage response = await client.DeleteAsync(deleteUri);
            return response;
        }

        [TestMethod]
        public async Task ReturnNotFoundForGetWithInValidId()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                string getUri = string.Format("stores/{0}", Guid.Empty);

                // act
                HttpResponseMessage response = await client.GetAsync(getUri);
                
                // assert
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }


        [TestMethod]
        public async Task ReturnBadRequestForAPostWithRedModel()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                //act 
                StoreModel storeModel = RedStoreModel();
                HttpResponseMessage response = await client.PostAsJsonAsync("stores", storeModel);
                // assert
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            }
        }

        [TestMethod]
        public async Task ReturnBadRequestForAPutWithRedModel()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                //act 
                StoreModel storeModel = RedStoreModel();
                string putUri = string.Format("stores/{0}", storeModel.Id);
                HttpResponseMessage response = await client.PutAsJsonAsync(putUri, storeModel);
                // assert
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            }
        }


        [TestMethod]
        public async Task ReturnBadRequestForDelete()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                //act 
                StoreModel storeModel = RedStoreModel();
                HttpResponseMessage response = await client.PostAsJsonAsync("stores", storeModel);
                // act 
                string deleteUri = string.Format("stores/{0}", storeModel.Id);
                response = await client.DeleteAsync(deleteUri);

                // assert
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }


    }
}
