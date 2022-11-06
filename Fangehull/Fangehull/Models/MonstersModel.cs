namespace Fangehull.Models
{
    public class MonstersModel
    {
        public int IdMonster { get; set; }
        public string MonsterName { get; set; }
        public int MaxHealtPoints { get; set; }
        public Difficulty Dificulty { get; set; }
    }
    public enum Difficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
        Extreme = 3,
        Insane = 4,
    }
}
