using ArmorFeedApi.Enterprises.Domain.Services.Communication;
using ArmorFeedApi.Enterprises.Resources;
using ArmorFeedApi.Vehicles.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SpecFlow.Internal.Json;
using System.Net.Mime;
using System.Text;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace ArmorFeedTest
{
    [Binding]
    class VehicleAssignmentStepsDefinition
    {
        private readonly WebApplicationFactory<Program> _factory;

        private HttpClient Client { get; set; }
        private Uri BaseUri { get; set; }
        private Task<HttpResponseMessage> Response { get; set; }
        private VehicleResource Course { get; set; }
        private EnterpriseResource Item { get; set; }
        private VehicleAssignmentStepsDefinition(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/sign-up is available")]
        public void GivenTheEndpointHttpsLocalhostApiVSignUpIsAvailable(int port, int enterpriseId)
        {
            BaseUri = new Uri($"http://localhost:{port}/api/v1/enterprise/{enterpriseId}/vehicles");
            Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
        }

        [Given(@"A Enterprise is already stored in Enterpise's Data")]
        public async void GivenAEnterpriseIsAlreadyStoredInEnterpiseSData(Table existingEnterpriseResource)
        {
            var directorUri = new Uri($"http://localhost:5017/api/v1/enterprises/sign-up");
            var resource = existingEnterpriseResource.CreateSet<RegisterEnterpriseRequest>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            await Client.PostAsync(directorUri, content);
        }

        [Given(@"A Customer is already stored in Customer's Data")]
        public async void GivenACustomerIsAlreadyStoredInCustomerSData(Table existingCustomerResource)
        {
            var directorUri = new Uri($"http://localhost:5017/api/v1/customers/sign-up");
            var resource = existingCustomerResource.CreateSet<RegisterEnterpriseRequest>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            await Client.PostAsync(directorUri, content);
        }

        [Given(@"A Shipment is already stored in Shipment's Data")]
        public async void GivenAShipmentIsAlreadyStoredInShipmentSData(Table existingShipmentResource)
        {
            var directorUri = new Uri($"http://localhost:5017/api/v1/customers/sign-up");
            var resource = existingShipmentResource.CreateSet<RegisterEnterpriseRequest>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            await Client.PostAsync(directorUri, content);
        }

        [When(@"A Post Request is sent")]
        public async Task WhenAPostRequestIsSent(Table existingDirectorResource)
        {
            var directorUri = new Uri($"http://localhost:7017/api/v1/vehicles");
            var resource = existingDirectorResource.CreateSet<SaveVehicleResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            await Client.PostAsync(directorUri, content);
        }

        [Then(@"A Response with Status (.*) is Received")]
        public async Task ThenAResponseWithStatusIsReceived(string expectedMessage)
        {

            var jsonExpectedMessage = expectedMessage.ToJson();
            var actualMessage = Response.Result.Content.ReadAsStringAsync().Result;
            var validMessage = actualMessage.Contains(jsonExpectedMessage);
            Assert.True(validMessage);
        }
    }
}
