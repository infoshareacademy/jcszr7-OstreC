using System.Security.AccessControl;

namespace OstreCWEB.ViewModel.Api
{
    public class Filter
    {
        public string SearchByInt { get; set; }

        public string ParamToOrder { get; set; } 
        public IEnumerable<string> ParamList { get; set; }

        public string SearchByName { get; set; }

        public int Limit { get; set; }

        public Filter()
        {
            SearchByInt = "";
            ParamToOrder = "";
            SearchByName = "";
            Limit = 0;
        }
    }
}
