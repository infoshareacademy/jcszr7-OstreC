using OstreCWeb.DomainModels.ApiContracts;

namespace OstreCWEB.Services
{
    public interface IFithEditionApiClient
    {
        public Task<SpellResponse> GetSpells();
    }
}
