using Newtonsoft.Json;
using OstreCWeb.DomainModels.ApiContracts;
using OstreCWEB.ViewModel.Api;
using System.IdentityModel.Tokens.Jwt;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OstreCWEB.Services.Api
{
    public class FithEditionApiClient : IFithEditionApiClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public FithEditionApiClient(IHttpClientFactory client)
        {
            _clientFactory = client;
        }
        public async Task<FithEditionApiResponse> GetSpells(int destinationPage)
        {
            return await MakeCall(null,null,destinationPage);
        }
        public async Task<FithEditionApiResponse> GetSpells(Filter? filter,int destinationPage)
        {
            return await MakeCall(null,filter, destinationPage);
        }
        private async Task<FithEditionApiResponse> MakeCall(string? query,Filter? filter, int destinationPage)
        {
            FithEditionApiResponse deserialized;
            query ??= await GenerateQuery(filter, destinationPage); 

            try
            {
                var response = await _clientFactory.CreateClient(TestRestApiConfiguration.TestRestApiClientName).GetAsync($"{query}");
                string responseBody = await response.Content.ReadAsStringAsync();
                deserialized = JsonConvert.DeserializeObject<FithEditionApiResponse>(responseBody);
                deserialized.ActivePage = destinationPage;
                //100 is default page size for D&D API
                double pageCount = filter != null && filter.Limit != 0 ? (double)deserialized.Count / filter.Limit : (double)deserialized.Count / 100; 
                deserialized.TotalPagesNumber = (int)Math.Ceiling(pageCount);
                deserialized.NextPage = await GetPageInt(deserialized.Next);
                deserialized.PreviousPage = destinationPage != 2 ? await GetPageInt(deserialized.Previous) : 1;
                deserialized.Filters = filter != null ? filter : new Filter();
            }
            catch
            {
                throw new Exception("Failed to fetch API data");
            } 
           

            return deserialized;

        }

        private async Task<string> GenerateQuery( Filter? filter, int destinationPage)
        {
            //Default API query for 1st page is an empty link 
            var query = "spells/?";
            query += destinationPage != 0 && destinationPage != 1 ? $"page={destinationPage}&" : "";
            var filters = filter != null ? await GetQueryFromFilters(filter) : "";
            query += filters;
            return query;
        }
        public async Task<int> GetPageInt(string pageLink )
        {
            var result = 0; 

            if(pageLink != null)
            {
                var linkArray = pageLink.Split('/');
                if (!String.IsNullOrEmpty(linkArray[4]))
                {
                    if (!linkArray[4].Contains("&"))
                    {
                        var parsenext = Int32.TryParse(linkArray[4].Substring(linkArray[4].Length - 1), out result);
                    }
                    else
                    {
                        var subLinkArray = pageLink.Split('&');
                        foreach (var filters in subLinkArray)
                        {
                            if (filters.Contains("page"))
                            {
                                var parsenext = Int32.TryParse(filters.Substring(filters.Length - 1), out result);
                            }
                        }
                        
                    }


                }   
            }
            return result;
        }

        public async Task<string> GetSearchString(IEnumerable<string> list)
        {
            return list.ToString();
        }

        private async Task<string> GetQueryFromFilters(Filter? filter)
        {
            var query = "";
            query += filter.Limit == 0 ?  "": $"limit={filter.Limit}&";//Limits and pagination
            query += String.IsNullOrEmpty(filter.ParamToOrder)  ?  "": $"ordering={filter.ParamToOrder}&"; //Ordering alphabetically for given parameter
            query += String.IsNullOrEmpty(filter.SearchByName) ? "" : $"search={filter.SearchByName}&";//Ressource search
            query += String.IsNullOrEmpty(filter.SearchByInt) ? "" : $"level_int={filter.SearchByInt}"; //Filter by int level
            return query;
        } 
    }
}