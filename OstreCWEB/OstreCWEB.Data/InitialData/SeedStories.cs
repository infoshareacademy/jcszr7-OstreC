using OstreCWEB.Repository.DataBase;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Enums;
using OstreCWEB.DomainModels.StoryModels.Properties;

namespace OstreCWEB.Repository.InitialData
{
    public static class SeedStories
    {
        public static void Seed(OstreCWebContext context, User user)
        {
            if (context.Stories.Any())
            {
                return;   // DB has been seeded
            }

            context.Stories.Add(
                new Story
                {
                    User = user,
                    Name = "Lost Mine Of Phandelver",
                    Description = "Lost Mine of Phandelver is an adventur is set a short distance from the city of Neverwinter in the Sword Coast region of the Forgotten Realms setting.",
                    Paragraphs = new List<Paragraph>
                    {
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.DescOfStage,
                            RestoreRest = false,
                            StageDescription = "You die, load the game or start over!",
                        },
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.DescOfStage,
                            RestoreRest = false,
                            StageDescription = "In the city of Neverwinter, a dwarf named Gundren Rockseeker asked you to bring a wagon load of provisions to the rough-and-tumble settlement of Phandalin, a couple of days' travel southeast of the city. Gundren was clearly excited and more than a little secretive about his reasons for the trip, saying only that he and his brothers had found \"something big\", and that he'd pay you ten gold pieces each for escorting his supplies safely to Barthen's Provisions, a trading post in Phandalin. He then set out ahead of you on horse, along with a warrior escort named Sildar Haliwinter, claiming he needed to arrive early to \"take care of business.\" You've spent the last few days following the High Road south from Neverwinter, and you've just recently veered east along the Triboar Trail. You've encountered no trouble so far, but this territory can be dangerous. Bandits and outlaws have been known to lurk along the trail.",
                            Choices = new List<Choice>
                            {
                                new Choice
                                {
                                    ChoiceText = "What's on the cart?",
                                    NextParagraphId = 3
                                },
                                new Choice
                                {
                                    ChoiceText = "Next",
                                    NextParagraphId = 4
                                },
                            }
                        },
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.DescOfStage,
                            RestoreRest = true,
                            StageDescription = "The wagon is packed full of an assortment of mining supplies and food. This includes a dozen sacks of flour, several casks ofsalted pork, two kegs ofstrong ale, shovels, picks, and crowbars (about a dozen each), and five lanterns with a small barrel of oil (about fiftyflasks in volume). The total value of the cargo is 100 gp.",
                            Choices = new List<Choice>
                            {
                                new Choice
                                {
                                    ChoiceText = "Next",
                                    NextParagraphId = 4
                                }
                            },
                            ParagraphItems = new List<ParagraphItem>
                            {
                                new ParagraphItem
                                {
                                    AmountOfItems = 2,
                                    Item = context.Items.FirstOrDefault(x => x.Name == "Healing Potion"),
                                    ParagraphId = 3
                                }
                            }
                        },
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.DescOfStage,
                            RestoreRest = true,
                            StageDescription = "You've been on the Triboar Trail for about half a day. As you come around a bend, you spot two dead horses sprawled about fifty feet ahead of you, blocking the path. Each has several black-feathered arrows sticking out of it. The woods press close to the trail here, with a steep embankment and dense thickets on either side.",
                            Choices = new List<Choice>
                            {
                                new Choice
                                {
                                    ChoiceText = "Look around",
                                    NextParagraphId = 5
                                },
                                new Choice
                                {
                                    ChoiceText = "Come closer",
                                    NextParagraphId = 6
                                },
                            }
                        },
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.Test,
                            RestoreRest = false,
                            StageDescription = "Perception test to see goblins",
                            TestProp = new TestProp
                            {
                                AbilityScores = AbilityScores.Wisdom,
                                TestDifficulty = TestDifficulty.Easy
                            },
                            Choices = new List<Choice>
                            {
                                new Choice
                                {
                                    ChoiceText = "Failure",
                                    NextParagraphId = 6
                                },
                                new Choice
                                {
                                    ChoiceText = "Success",
                                    NextParagraphId = 7
                                },
                            }
                        },
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.DescOfStage,
                            RestoreRest = false,
                            StageDescription = "The horses as belonging to Gundren Rockseeker and Sildar Hallwinter. They've been dead about a day, and it's clear that arrows killed the horses.",
                            Choices = new List<Choice>
                            {
                                new Choice
                                {
                                    ChoiceText = "Get closer to the dead horses",
                                    NextParagraphId = 8
                                }
                            }
                        },
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.DescOfStage,
                            RestoreRest = false,
                            StageDescription = "Two goblins are hiding in the woods, one on each side of the road.",
                            Choices = new List<Choice>
                            {
                                new Choice
                                {
                                    ChoiceText = "Get closer to the dead horses",
                                    NextParagraphId = 8
                                },
                                new Choice
                                {
                                    ChoiceText = "Attack goblins",
                                    NextParagraphId = 9
                                }
                            }
                        },
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.DescOfStage,
                            RestoreRest = false,
                            StageDescription = "The saddlebags have been looted. Nearby lies an empty 1 leather map case.",
                            Choices = new List<Choice>
                            {
                                new Choice
                                {
                                    ChoiceText = "Next",
                                    NextParagraphId = 9
                                },
                            }
                        },
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.Fight,
                            RestoreRest = false,
                            StageDescription = "Fight with 2 goblin",
                            FightProp = new FightProp
                            {
                                ParagraphEnemies = new List<EnemyInParagraph>
                                {
                                    new EnemyInParagraph
                                    {
                                        AmountOfEnemy = 2,
                                        EnemyId = 1
                                        //EnemyName = "Goblin"
                                    }
                                }
                            },
                            Choices = new List<Choice>
                            {
                                new Choice
                                {
                                    ChoiceText = "Defeat!",
                                    NextParagraphId = 1
                                },
                                new Choice
                                {
                                    ChoiceText = "Victory!",
                                    NextParagraphId = 10
                                },
                            }
                        },
                        new Paragraph
                        {
                            ParagraphType = ParagraphType.DescOfStage,
                            RestoreRest = true,
                            StageDescription = "You are looking at two dead goblins.",
                            Choices = new List<Choice>
                            {
                                new Choice
                                {
                                    ChoiceText = "Nothing else!",
                                    NextParagraphId = 1
                                }
                            }
                        }
                    },
                    FirstParagraphId = 2,
                }); ;
            context.SaveChanges();
        }
    }
}
