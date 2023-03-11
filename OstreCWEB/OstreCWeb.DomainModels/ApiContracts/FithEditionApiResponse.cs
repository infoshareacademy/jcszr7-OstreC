using OstreCWEB.ViewModel.Api;

namespace OstreCWeb.DomainModels.ApiContracts
{
    public class FithEditionApiResponse
    {
        public int Count { get; set; }
        public int TotalPagesNumber { get; set; }
        public string Next { get; set; }
        public string? Previous { get; set; } 
        public int? ActivePage { get; set; }
        public List<SpellResponseItem> Results { get; set; }
    }
}
