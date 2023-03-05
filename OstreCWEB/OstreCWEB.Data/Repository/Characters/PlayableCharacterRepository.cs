using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class PlayableCharacterRepository : IPlayableCharacterRepository
    {
        private OstreCWebContext _db;
        public PlayableCharacterRepository(OstreCWebContext db)
        {
            _db = db;
        }

        public Task<PlayableCharacter> Create(PlayableCharacter playableCharacter)
        {
            _db.PlayableCharacters.Add(playableCharacter);
            _db.SaveChanges();
            return Task.FromResult(playableCharacter);
        }
        public bool Exists(int id)
        {
            return _db.PlayableCharacters.Any(x => x.CharacterId == id);
        }

        public async Task DeleteAsync(PlayableCharacter playableCharacter)
        {
            _db.PlayableCharacters.Remove(playableCharacter);
            await _db.SaveChangesAsync();
        }

        public async Task<List<PlayableCharacter>> GetAllTemplatesAsync()
        {
            return await _db.PlayableCharacters.ToListAsync();
        }
        /// <summary>
        /// Gets all playable characters except those belonging to a given user.
        /// </summary>
        /// <param name="userCharacters"></param>
        /// <returns></returns>
        public async Task<List<PlayableCharacter>> GetAllTemplatesExceptAsync(int userId)
        {
            return await _db.PlayableCharacters.Where(c => c.UserId != userId && c.IsTemplate == true).ToListAsync();
        }

        public async Task<PlayableCharacter> GetByIdAsync(int id)
        {
            return await _db.PlayableCharacters.FirstAsync(c => c.CharacterId == id);
        }
        public async Task UpdateAsync(PlayableCharacter playableCharacter)
        {
            _db.PlayableCharacters.Update(playableCharacter);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAlreadyTrackedAsync(PlayableCharacter playableCharacter)
        {
            var tracked = await _db.PlayableCharacters.FindAsync(playableCharacter.CharacterId);
            tracked = playableCharacter;
        }
        public async Task<PlayableCharacter> GetByIdNoTrackingAsync(int characterTemplateId)
        {
            return await _db.PlayableCharacters
                 .Include(x => x.CharacterClass)
                 .ThenInclude(y => y.ActionsGrantedByClass)
                 .Include(x => x.CharacterClass)
                 .ThenInclude(y => y.ItemsGrantedByClass)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(x => x.CharacterId == characterTemplateId);
        }
        #region
        public void CreateNew(PlayableCharacter model)
        {
            _db.PlayableCharacters.Add(model);
            _db.SaveChanges();
        }
        public List<PlayableRace> GetAllRaces()
        {
            return _db.PlayableCharacterRaces.ToList();
        }
        public List<PlayableClass> GetAllClasses()
        {
            return _db.PlayableCharacterClasses.ToList();
        }
        public void RollAttributes(PlayableCharacter model)
        {
            var calc = model;
            calc.Strenght = RollDice();
            calc.Dexterity = RollDice();
            calc.Constitution = RollDice();
            calc.Intelligence = RollDice();
            calc.Wisdom = RollDice();
            calc.Charisma = RollDice();
        }
        private int RollDice(int maxValue = 7)
        {
            int[] rolls = new int[4];

            Random rng = new Random();
            for (int i = 0; i < rolls.Length; i++)
            {
                rolls[i] = rng.Next(1, maxValue);
            }
            Array.Sort(rolls);
            Array.Reverse(rolls);

            int sum = rolls.Take(3).Sum();
            return sum;
        }
        public List<string> GetAllNames()
        {
            return maleNames;
        }
        public string GetClassDescription(int id)
        {
            return classesDescription[id];
        }
        public string GetRaceDescription(int id)
        {
            return racesDescription[id];
        }

        #endregion

        #region Data
        List<string> maleNames = new List<string>()
        {
            "Aiden", "Ethan", "Jacob", "Michael", "William",
            "Alexander", "Daniel", "Matthew", "Nicholas", "Christopher",
            "Andrew", "Brandon", "Caleb", "David", "Elijah",
            "Isaac", "Gabriel", "Josiah", "Luke", "Noah"
        };

        List<string> classesDescription = new List<string>()
        {
            //Fighter
            "A fighter is a character class that focuses on combat prowess and physical prowess." +
                " Fighters are versatile characters that can wield a variety of weapons and armor and have the ability to adapt to different battle situations." +
                " They have a high hit point value and strong defense, making them formidable in close combat. They can also serve as the partys primary defender," +
                " using their combat skills and bravery to protect their allies. With a variety of fighting styles, weapons, and armor options, the fighter class is" +
                " ideal for players who enjoy engaging in direct combat and tactical battles.",
            //Wizard
            "The Wizard is a character class in DnD that specializes in casting spells. Wizards have a wide range of spells at their disposal," +
                " which they can use to attack their enemies, heal their allies, or manipulate the environment to their advantage." +
                " Wizards have access to a spellbook that contains the spells they know and they can learn new spells as they gain experience." +
                " Wizards are known for their intelligence and their mastery of magic, but they are often physically weak and vulnerable in combat." +
                " To compensate for their lack of physical prowess, they rely on their spells and their ability to use them strategically in battle." +
                " Wizards are versatile characters who can adapt to a wide range of play styles and can be effective in many different types of adventures." +
                " Overall, the Wizard class is well-suited for players who enjoy casting spells, solving puzzles, and using their wits to overcome obstacles.",
            //Cleric
            "Clerics are divine spellcasters who draw their power from a deity or pantheon of gods. They are often seen as holy warriors or healers," +
                " and their spells focus on supporting their allies and defeating their enemies. Clerics have access to a wide range of spells," +
                " including those that can heal wounds, remove curses, or smite their enemies. They also have the ability to turn undead creatures," +
                " which makes them powerful allies in battles against the undead. Clerics are able to use heavy armor and weapons," +
                " which makes them more durable in combat than other spellcasting classes. In addition to their spells," +
                " Clerics also have the ability to channel divine energy to fuel their class abilities. This allows them to perform powerful attacks or grant bonuses to their allies." +
                " Clerics are also capable of performing rituals, which can have a wide range of effects, such as creating magical portals or calling forth powerful spirits." +
                " Clerics are a good choice for players who enjoy playing supportive characters or who want a character that is capable of both casting spells and participating in combat." +
                " They are versatile and can be adapted to a wide range of play styles, from supporting their allies as healers to leading the charge as holy warriors."
        };

        List<string> racesDescription = new List<string>()
        {
            //Humans
            "Humans are one of the most versatile and common races in DnD. They are known for their quick adaptability," +
            " their ability to learn new skills quickly, and their diverse cultural backgrounds. Humans are also known for their ambition and their drive to succeed," +
            " which makes them a popular choice for players who want to create a character with a wide range of possibilities. In game mechanics," +
            " Humans have an ability score bonus, which means that they receive a bonus to one or more of their ability scores," +
            " such as Strength, Dexterity, or Intelligence. They also have the ability to choose a bonus feat at 1st level," +
            " which allows them to gain a special ability or skill that sets them apart from other races. Overall," +
            " Humans are a good choice for players who want a versatile and adaptable character," +
            " or for those who want a character that is similar to themselves in terms of abilities and personality." +
            " They can fit into a wide range of play styles and can be effective in many different types of adventures.",
            //Elves
            "Elves are a long-lived race known for their grace, beauty, and skill with magic. They are often depicted as being deeply connected to the natural world," +
            " and they live in forests, glades, and other sylvan settings. Elves have several unique traits and abilities that set them apart from other races." +
            " They are nimble and quick, with a bonus to their Dexterity score, and they have keen senses, making them excellent scouts and archers." +
            " Elves also have resistance to magic, which makes them more resistant to spells and other magical effects. In addition to their physical abilities," +
            " Elves also have a strong connection to magic. Many Elves are born with the ability to cast spells, and they have a natural affinity for magic that makes them powerful arcane spellcasters." +
            " They also have an innate understanding of the natural world, which allows them to move quickly and quietly through forests and other wilderness areas." +
            " Overall, Elves are a good choice for players who want a character that is graceful, quick, and capable of casting spells." +
            " They are ideal for players who enjoy playing characters that are deeply connected to the natural world, or for those who want a character that is highly skilled in archery or stealth." +
            " Elves can be adapted to a wide range of play styles, from cunning rangers to powerful wizards, and they are well-suited for many different types of adventures.",
            //Dwarves
            "Dwarves are a race in Dungeons and Dragons known for their skill in mining, metalworking, and their love of treasure." +
            " They are a sturdy and resilient people, with a bonus to their Constitution score, and they are known for their courage and determination." +
            " Dwarves live in underground cities and mines, where they work tirelessly to extract valuable minerals and gems from the earth." +
            " They are skilled craftsmen, and they are known for their ability to create high-quality weapons and armor. They also have a strong sense of community," +
            " and they value their friends and family above all else. In combat, Dwarves are known for their toughness and their ability to absorb damage." +
            " They are also skilled in the use of axes and hammers, which makes them formidable in close combat. They have the ability to move quickly through difficult terrain," +
            " such as mountains and caverns, and they can see in the dark, which makes them effective in underground environments." +
            " Overall, Dwarves are a good choice for players who want a character that is tough, resilient, and capable of wielding powerful weapons." +
            " They are ideal for players who enjoy playing characters that are highly skilled in crafting and mining," +
            " or for those who want a character that is deeply connected to the earth and its treasures. Dwarves can be adapted to a wide range of play styles," +
            " from brave warriors to cunning thieves, and they are well-suited for many different types of adventures."
        };

        #endregion



    }
}
