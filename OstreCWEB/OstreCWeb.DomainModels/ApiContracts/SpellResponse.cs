using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWeb.DomainModels.ApiContracts
{
    public class SpellResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string? Previous { get; set; }
        public List<SpellResponseItem> Results { get; set; }
    }
}
