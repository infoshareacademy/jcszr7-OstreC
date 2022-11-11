using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Fangehull.Data;
using System;
using System.Linq;

namespace Fangehull.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FangehullContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FangehullContext>>()))
            {
                // Look for any movies.
                if (context.Monster.Any())
                {
                    return;   // DB has been seeded
                }

                context.Monster.AddRange(
                new Monster
                {
                    MonsterName = "Goblin",
                    MaxHealtPoints = 10,
                    Difficulty = Difficulties.Easy
                },
                new Monster
                {
                    MonsterName = "Orc",
                    MaxHealtPoints = 20,
                    Difficulty = Difficulties.Normal
                },
                new Monster
                {
                    MonsterName = "Bandit",
                    MaxHealtPoints = 50,
                    Difficulty = Difficulties.Hard
                },
                new Monster
                {
                    MonsterName = "Golem",
                    MaxHealtPoints = 200,
                    Difficulty = Difficulties.Extreme
                },
                new Monster
                {
                    MonsterName = "Dragon",
                    MaxHealtPoints = 450,
                    Difficulty = Difficulties.Insane
                }
                );
                context.SaveChanges();
            }
        }
    }
}
