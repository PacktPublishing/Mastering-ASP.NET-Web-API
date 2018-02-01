using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Puhoi.Models.Models;
using RestSharp.Serializers;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PuhoiAPI.xUnit.Tests
{
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

        private readonly string _baseUri;
        private const string _configurationBaseUri = "baseUri";
        private readonly JsonSerializer _jsonSerializer;


        public StoresControllerShould()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            _baseUri = config[_configurationBaseUri];
            _jsonSerializer = new JsonSerializer();
        }

        private HttpContent SerializeModelToHttpContent(object obj)
        {
            string storeModelJSon = _jsonSerializer.Serialize(obj);
            
            HttpContent stringContent = new StringContent(
            storeModelJSon,
            Encoding.UTF8,
            "application/json");

            return stringContent;
        }

        [Fact]
        public async void ReturnOkForHealthCheck()
        {

            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                //act 
                HttpResponseMessage response = await client.GetAsync("stores/healthcheck");

                // assert
                Assert.NotNull(response);
                Assert.Equal(response.StatusCode, HttpStatusCode.OK);
            }
        }

        [Fact]

        public async Task ReturnCreatedForAPost()
        {
            
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                StoreModel storeModel = GreenStoreModel();

                HttpContent httpContent = SerializeModelToHttpContent(storeModel);

                //act 
                try
                {
                    HttpResponseMessage response = await client.PostAsync("stores", httpContent);

                    // assert
                    Assert.NotNull(response);
                    Assert.Equal(response.StatusCode, HttpStatusCode.Created);

                }
                catch (Exception ex)
                {

                    throw;
                }
                await DeleteStore(client,storeModel.Id);
            }
        }

        [Fact]
        public async Task ReturnConflictForAPost()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                
                StoreModel storeModel = GreenStoreModel();
                storeModel.Name = "ConflictStore";


                HttpContent httpContent = SerializeModelToHttpContent(storeModel);

                //act 
                HttpResponseMessage response = await client.PostAsync("stores", httpContent);

                //arrange 
                httpContent = SerializeModelToHttpContent(storeModel);


                // act 
                response = await client.PostAsync("stores", httpContent);


                // assert
                response.StatusCode.Should().Be(HttpStatusCode.Conflict);

                // clean up
                await DeleteStore(client, storeModel.Id);
            }
        }

        [Fact]
        public async Task ReturnOkForAPut()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                
                StoreModel storeModel = GreenStoreModel();
                storeModel.Id = Guid.NewGuid();

               
                HttpContent httpContent = SerializeModelToHttpContent(storeModel);

                //act 
                HttpResponseMessage response = await client.PostAsync("stores", httpContent);

                //arrange 
                storeModel.Name = "Creamy Cheese";

                 httpContent = SerializeModelToHttpContent(storeModel);

                // act 
                string putUri = string.Format("stores/{0}", storeModel.Id);
                response = await client.PutAsync(putUri, httpContent);

                // assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                // clean up
                await DeleteStore(client, storeModel.Id);
            }
        }


        [Fact]
        public async Task ReturnOkForDelete()
        {
            // arrange 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                
                StoreModel storeModel = GreenStoreModel();
                storeModel.Id = Guid.NewGuid();

                HttpContent httpContent = SerializeModelToHttpContent(storeModel);

                //act 
                HttpResponseMessage response = await client.PostAsync("stores", httpContent);

                // act 
                response = await DeleteStore(client, storeModel.Id);

                // assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        private async Task<HttpResponseMessage> DeleteStore(HttpClient client , Guid Id)
        {
            string deleteUri = string.Format("stores/{0}", Id);
            HttpResponseMessage response =  await client.DeleteAsync(deleteUri);
            return response;
        }

        [Fact]
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

    }
}
