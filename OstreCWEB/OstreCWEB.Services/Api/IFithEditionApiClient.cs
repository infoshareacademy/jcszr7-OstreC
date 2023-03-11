using OstreCWeb.DomainModels.ApiContracts;
using OstreCWEB.ViewModel.Api;

namespace OstreCWEB.Services.Api
{
    public interface IFithEditionApiClient
    {
        public Task<FithEditionApiResponse> GetSpells(Filter? filter,int page);
    }
}
