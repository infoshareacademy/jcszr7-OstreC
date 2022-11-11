using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fangehull.Models
{
    public class Monster
    {
        public int Id { get; set; }
        [Display(Name = "Monster Name")]
        public string MonsterName { get; set; }
        [Display(Name = "Max Healt Points")]
        public int MaxHealtPoints { get; set; }

        //[Required]
        //public string DifficultyAsString
        //{
        //    get
        //    {
        //        return this.Difficulty.ToString();
        //    }
        //    set
        //    {
        //        Difficulty = (Difficulties)Enum.Parse(typeof(Difficulties), value, true);
        //    }
        //}
        public Difficulties Difficulty { get; set; }

    }
    public enum Difficulties
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
        Extreme = 3,
        Insane = 4,
    }
}
