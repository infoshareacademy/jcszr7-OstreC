using System.ComponentModel;

namespace OstreCWEB.ViewModel.Api
{
    public class SpellView
    {
        [DisplayName("Name")] 
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Desc { get; set; }
        [DisplayName("Empowered effect")]
        public string Higher_level { get; set; }
        [DisplayName("Page")]
        public string Page { get; set; }
        [DisplayName("Range")]
        public string Range { get; set; }
        [DisplayName("Concentration")]
        public string Concentration { get; set; }
        [DisplayName("Casting Time")]
        public string Casting_time { get; set; }
        [DisplayName("Level")]
        public string Level { get; set; }
        [DisplayName("School")]
        public string School { get; set; }
        [DisplayName("DnD class")]
        public string Dnd_class { get; set; }
    }
}
