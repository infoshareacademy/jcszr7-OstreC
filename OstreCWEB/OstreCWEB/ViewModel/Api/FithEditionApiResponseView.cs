using System.ComponentModel;
using OstreCWeb.DomainModels.ApiContracts;

namespace OstreCWEB.ViewModel.Api
{
    public class FithEditionApiResponseView 
    {
        [DisplayName("Results found")]
        public int Count { get; set; }
        [DisplayName("Total Pages")]
        public int TotalPagesNumber { get; set; } 
        public string Next { get; set; }
        public string? Previous { get; set; }
        [DisplayName("Results")]
        public List<SpellView> Results { get; set; } 
        public int ActivePage { get; set; }
    }
}
