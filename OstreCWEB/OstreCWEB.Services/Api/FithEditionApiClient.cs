using Newtonsoft.Json;
using OstreCWeb.DomainModels.ApiContracts; 

namespace OstreCWEB.Services.Api
{
    public class FithEditionApiClient : IFithEditionApiClient
    {
        readonly IHttpClientFactory _clientFactory;
        public FithEditionApiClient(IHttpClientFactory client)
        {
            _clientFactory = client;
        }
        public async Task<SpellResponse> GetSpells()
        {
            var responseEasy = await _clientFactory.CreateClient(TestRestApiConfiguration.TestRestApiClientName).GetAsync("spells/?limit=100");
            string responseBody = await responseEasy.Content.ReadAsStringAsync();
            var deserialised = JsonConvert.DeserializeObject<SpellResponse>(responseBody);
            return deserialised; 
        }

        public async Task<string> GetSearchString(IEnumerable<string> list)
        {
            return list.ToString();
        }
    }
}
