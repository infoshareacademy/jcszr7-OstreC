using OstreCWeb.DomainModels.ApiContracts;
using OstreCWEB.ViewModel.Api;

namespace OstreCWEB.Services.Api
{
    public interface IFithEditionApiClient
    {
        public Task<FithEditionApiResponse> GetSpells(int destinationPage);
        public Task<FithEditionApiResponse> GetSpells(Filter? filter, int page);
        //public Task<FithEditionApiResponse> GetSpells(string query, int destinationPage,Filter? filter);
    }
}
