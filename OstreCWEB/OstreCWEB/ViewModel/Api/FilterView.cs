using System.ComponentModel;

namespace OstreCWEB.ViewModel.Api
{
    public class FilterView
    {
        [DisplayName("Search by level")]
        public string SearchByInt { get; set; }
        [DisplayName("Order by")]
        public string ParamToOrder { get; set; }
        [DisplayName("Search by name")]
        public string SearchByName { get; set; }
        [DisplayName("Results per page")]
        public int Limit { get; set; }
        public IEnumerable<string> ParamList { get; set; }
    }
}
