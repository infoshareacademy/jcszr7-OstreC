using Newtonsoft.Json;
using OstreCWeb.DomainModels.ApiContracts;
using OstreCWEB.ViewModel.Api;

namespace OstreCWEB.Services.Api
{
    public class FithEditionApiClient : IFithEditionApiClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public FithEditionApiClient(IHttpClientFactory client)
        {
            _clientFactory = client;
        }

        public async Task<FithEditionApiResponse> GetSpells(Filter? filter, int page)
        {
            var query = "spells/?";
            query += page != 0 ? $"page={page}" : "";
            var filters = await GetQueryFromFilters(filter);
            query += filters;
            
            var response = await _clientFactory.CreateClient(TestRestApiConfiguration.TestRestApiClientName).GetAsync($"{query}");
            string responseBody = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<FithEditionApiResponse>(responseBody);
            //100 is default page size for D&D API
            var pageCount = filter != null && filter.Limit != 0 ? (double)deserialized.Count / filter.Limit : deserialized.Count / 100;

            deserialized.TotalPagesNumber = (int)Math.Ceiling(pageCount);
            deserialized.ActivePage = page;
            var next = deserialized.Next.Split('/');
            if (!String.IsNullOrEmpty(next[4]))
            {
                var parsenext = Int32.TryParse(next[4].Substring(next[4].Length - 1), out int nextPage);
                if (parsenext)
                {
                    deserialized.NextPage = nextPage;
                }
            }
        
            else
            {
                deserialized.NextPage = 0;
            }
            return deserialized;
        }

        public async Task<string> GetSearchString(IEnumerable<string> list)
        {
            return list.ToString();
        }

        private async Task<string> GetQueryFromFilters(Filter? filter)
        {
            var query = "";
            query += filter.Limit == 0 ?  "": $"limit={filter.Limit}&";
            query += String.IsNullOrEmpty(filter.SearchByName)  ?  "": $"ordering={filter.SearchByName}&";
            query += String.IsNullOrEmpty(filter.SortByParam) ? "" : $"search={filter.SortByParam}&";
            query += String.IsNullOrEmpty(filter.SearchByInt) ? "" : $"filter={filter.SearchByInt}&"; 
            return query;
        }
    }
}