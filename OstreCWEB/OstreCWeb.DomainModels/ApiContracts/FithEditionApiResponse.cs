using OstreCWEB.ViewModel.Api;

namespace OstreCWeb.DomainModels.ApiContracts
{
    public class FithEditionApiResponse
    {
        public int Count { get; set; }
        public int TotalPagesNumber { get; set; }
        public int Next { get; set; }
        public int Previous { get; set; } 
        public int? ActivePage { get; set; }
        public List<SpellResponseItem> Results { get; set; }
    }
}
