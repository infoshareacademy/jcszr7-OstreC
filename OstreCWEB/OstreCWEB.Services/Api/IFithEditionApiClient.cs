using OstreCWeb.DomainModels.ApiContracts;

namespace OstreCWEB.Services.Api
{
    public interface IFithEditionApiClient
    {
        public Task<SpellResponse> GetSpells();
    }
}
