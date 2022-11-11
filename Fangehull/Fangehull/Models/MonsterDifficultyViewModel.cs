using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Fangehull.Models
{
    public class MonsterDifficultyViewModel
    {
        public List<Monster>? Monsters { get; set; }
        public SelectList? Difficulties { get; set; }
        public string? MonsterDifficulty { get; set; }
        public string? SearchString { get; set; }
    }
}
