using Newtonsoft.Json;
using OstreCWeb.DomainModels.ApiContracts;
using OstreCWeb.Services;
using System.Text.Json.Nodes;

namespace OstreCWEB.Services
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
            //try
            //{
            //    using HttpResponseMessage response = await client.GetAsync("https://api.open5e.com/spells/?limit=100");
            //    response.EnsureSuccessStatusCode();
            
            // Above three lines can be replaced with new helper method below
            //    // string responseBody = await client.GetStringAsync(uri);

            //    var deserialised = JsonConvert.DeserializeObject<SpellResponse>(responseBody);
            //    return deserialised;
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine("\nException Caught!");
            //    Console.WriteLine("Message :{0} ", e.Message);
            //    throw;
            //}

        }
        //https://api.open5e.com/monsters/?limit=100
    }
}
