﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreC.Services.Characters
{
    public class CreatePlayer
    {
        Player player = new Player();
        List<Player> playerList = new List<Player>();
        public List<int> attributePoints = new List<int>();

        public bool isPlayerCreated = false;
        string name;
        string race;
        string charClass;
        int strength;
        int dexterity;
        int constitution;
        int intelligence;
        int wisdom;
        int charisma;

        ConsoleColor ccWhite = ConsoleColor.White;
        ConsoleColor ccYellow = ConsoleColor.Yellow;
        ConsoleColor ccRed = ConsoleColor.Red;
        //public void Create()
        //{
        //    while (true)
        //    {
        //        Utilities.WriteLineColorText("What do you want to do?", ccWhite, consoleClear: true);
        //        Console.WriteLine("1. Use already created adventurer\n2. Create your own adventurer\n3. Delete your adventurer\n4. Display statistics\n0. Test functionality");
        //        int.TryParse(Console.ReadLine(), out int input);
        //        switch (input)
        //        {
        //            case 1:
        //                CreateDefaultPlayer();
        //                break;
        //            case 2:
        //                //CreateCustomPlayer();
        //                break;
        //            case 3:
        //                DeletePlayer();
        //                break;
        //            case 4:
        //                DisplayStatistics();
        //                break;
        //            case 9:
        //                GenerateAttributePoints();
        //                break;
        //            case 0:
        //                GenerateAttributePoints();
        //                //AddAttributePoints(Attributes.Strength);
        //                DisplayListAttributes(attributePoints);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}
        public void CreateDefaultPlayer()
        {
            playerList.Clear();
            playerList.Add(new Player
            {
                Name = "Jaheira",
                Race = "Human",
                CharClass = "Warrior",
                HealthPoints = 10,
                Level = 1,
                Strength = 18,
                Dexterity = 17,
                Constitution = 18,
                Intelligence = 11,
                Wisdom = 14,
                Charisma = 16
            });
            isPlayerCreated = true;
        }
        //public void CreateCustomPlayer()
        //{
        //    //if (isPlayerCreated)
        //    //{
        //    //    Utilities.WriteLineColorText("Player already exists. You can have only 1 adventurer", ccRed, consoleClear: true);
        //    //    Utilities.PressAnyKey();
        //    //    return;
        //    //}
        //    //playerList.Clear();
        //    //Utilities.WriteLineColorText("Tell me your name: ", firstColor: ccWhite, consoleClear: true);
            
        //    //Utilities.WriteLineColorText("Choose your race: ", firstColor: ccWhite);
            
        //    //Utilities.WriteLineColorText("Choose your class: ", firstColor: ccWhite);

        //    //Console.WriteLine("Spend your points on attributes");
        //}
        public void AddName()
        {
            name = Utilities.InputDataAsString(Utilities.rgxAZ);
        }
        public void AddRace()
        {
            race = Console.ReadLine();
        }
        public void AddClass()
        {
            charClass = Console.ReadLine();
        }
        public void AddValueToProperties()
        {
            playerList.Add(new Player
            {
                Name = name,
                Race = race,
                CharClass = charClass,
                Strength = strength,
                Dexterity = dexterity,
                Constitution = constitution,
                Intelligence = intelligence,
                Wisdom = wisdom,
                Charisma = charisma,
            });
            isPlayerCreated = true;
        }

        public int AddAttributePoints(Attributes attribute)
        {
            //DisplayStatistics();
            int minValue = attributePoints.Min();
            //if (minValue == -1)
            //{
            //    classCounter++;
            //    minValue = attributePoints[classCounter];
            //}
            int maxValue = attributePoints.Max();
            //int minValue = 1;
            //int maxValue = 6;
            int input = Utilities.InputDataAsInt(minValue, maxValue);
            switch (attribute)
            {
                case Attributes.Strength:
                    ChangeValueFromList(attributePoints, input);
                    return strength = input;
                case Attributes.Dexterity:
                    ChangeValueFromList(attributePoints, input);
                    return dexterity = input;
                case Attributes.Constitution:
                    ChangeValueFromList(attributePoints, input);
                    return constitution = input;
                case Attributes.Intelligence:
                    ChangeValueFromList(attributePoints, input);
                    return intelligence = input;
                case Attributes.Wisdom:
                    ChangeValueFromList(attributePoints, input);
                    return wisdom = input;
                case Attributes.Charisma:
                    ChangeValueFromList(attributePoints, input);                    
                    return charisma = input;
                default:
                    return -1;
            }
        }
        public void DisplayListAttributes(List<int> list)
        {
            Console.Clear();
            int i = 32;
            Utilities.Underline('=', i);
            int counter = 0;
            foreach (var item in list)
            {
                counter++;
                if (counter == 1)
                    Console.Write("| ID  |");
                Console.Write($"{counter,3} ");
                if (counter == list.Count())
                    Console.Write("|");
            }
            Console.WriteLine("");
            Utilities.Underline('=', i);
            counter = 0;
            foreach (var item in list)
            {
                counter++;
                if (counter == 1)
                    Console.Write("| ATR |");
                //Console.Write($"{item,3} ");
                if (item == -1)
                    Utilities.WriteColorText($"  - ", firstColor: ConsoleColor.Yellow);
                else
                    Utilities.WriteColorText($"{item,3} ", firstColor: ConsoleColor.Yellow);
                if (counter == list.Count())
                    Console.Write("|");
            }
            Console.WriteLine("");
            Utilities.Underline('=', i);
        }
        public void RemoveValueFromList(List<int> list, int input)
        {
            list.Remove(input);
        }
        public void ChangeValueFromList(List<int> list, int input, bool isID = false)
        {
            if (isID)
            {
                if (list.Contains(input))
                {
                    list.RemoveAt(input - 1);
                    list.Insert(input - 1, -1);
                }
            }
            else
            {
                if (list.Contains(input))
                {
                    list.Insert(list.IndexOf(input), -1);
                    list.Remove(input);
                }                
            }
        }
        public enum Attributes
        {
            Strength,
            Dexterity,
            Constitution,
            Intelligence,
            Wisdom,
            Charisma
        }
        public void DeletePlayer()
        {
            playerList.Clear();
            isPlayerCreated = false;
        }
        public void GenerateAttributePoints()
        {
            if (!(attributePoints.Count() == 0))
            {
                attributePoints.Clear();
            }

            for (int i = 0; i < 6; i++)
            {
                List<int> valueRollDiceTemp = new List<int>();
                valueRollDiceTemp.Add(Utilities.DiceRoll(7));
                valueRollDiceTemp.Add(Utilities.DiceRoll(7));
                valueRollDiceTemp.Add(Utilities.DiceRoll(7));
                valueRollDiceTemp.Add(Utilities.DiceRoll(7));

                int lowestValues = valueRollDiceTemp.Min();
                //Console.WriteLine("lowestValues: " + lowestValues);
                int index = valueRollDiceTemp.IndexOf(lowestValues);
                //Console.WriteLine("index of lowValues: " + index);
                valueRollDiceTemp.RemoveAt(index);
                int sum = 0;

                foreach (var item in valueRollDiceTemp)
                {
                    sum += item;
                }
                attributePoints.Add(sum);
            }
        }
        public void DisplayStatistics()
        {
            //if (!isPlayerCreated)
            //{
            //    Utilities.WriteLineColorText("No adventurer to delete! im here", ccRed, true);
            //    Utilities.PressAnyKey();
            //    return;
            //}
            //Console.Clear();
            foreach (var item in playerList)
            {
                //Console.WriteLine($"Name: {item.Name}\nRace: {item.Race}\nClass: {item.CharClass}\nHealthPoints: {item.HealthPoints}\nLevel: {item.Level}\n" +
                //    $"Strength: {item.Strength}\nDexterity: {item.Dexterity}\nConstitution: {item.Constitution}\nIntelligence: {item.Intelligence}\n" +
                //    $"Wisdom: {item.Wisdom}\nCharisma: {item.Charisma}\n");
                Utilities.WriteLineColorText($"Name: ", $"{item.Name}", secondColor: ccYellow, multiplierTab: 2);
                Utilities.WriteLineColorText($"Race: ", $"{item.Race}", secondColor: ccYellow, multiplierTab: 2);
                Utilities.WriteLineColorText($"Class: ", $"{item.CharClass}", secondColor: ccYellow, multiplierTab: 2);
                Utilities.WriteLineColorText($"HP: ", $"{item.HealthPoints}", secondColor: ccYellow, multiplierTab: 2);
                Utilities.WriteLineColorText($"Level: ", $"{item.Level}", secondColor: ccYellow, multiplierTab: 2);
                Utilities.WriteLineColorText($"Strength: ", $"{item.Strength}", secondColor: ccYellow, multiplierTab: 1);
                Utilities.WriteLineColorText($"Dexterity: ", $"{item.Dexterity}", secondColor: ccYellow, multiplierTab: 1);
                Utilities.WriteLineColorText($"Constitution: ", $"{item.Constitution}", secondColor: ccYellow, multiplierTab: 1);
                Utilities.WriteLineColorText($"Intelligence: ", $"{item.Intelligence}", secondColor: ccYellow, multiplierTab: 1);
                Utilities.WriteLineColorText($"Wisdom: ", $"{item.Wisdom}", secondColor: ccYellow, multiplierTab: 1);
                Utilities.WriteLineColorText($"Charisma: ", $"{item.Charisma}", secondColor: ccYellow, multiplierTab: 1);
            }
            //Utilities.PressAnyKey();
        }
        public void DisplayStatistics(params string[] List)
        {
            foreach (var item in List)
            {
                Console.WriteLine(item);
            }
        }
        //public void DisplayStatistics()
        //{
        //    PropertyInfo[] myAttributesInfo;
        //    // Get the properties of 'Type' class object.
        //    myAttributesInfo = player.GetType().GetProperties();
        //    Console.WriteLine("Properties of System.Type are:");
        //    for (int i = 0; i < myAttributesInfo.Length; i++)
        //    {
        //        Console.WriteLine(myAttributesInfo[i].Name.ToString());
        //        attrList.Add(myAttributesInfo[i].Name.ToString());
        //    }
        //}
        ///Nie tykać, wybuchnie!!!
        //private void LoadDefaultData()
        //{
        //    try
        //    {
        //        foreach (var tuple in playerList.Zip(playerList, (x, y) => (x, y)))
        //        {
        //            tuple.x = tuple.y;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}
    }
}
