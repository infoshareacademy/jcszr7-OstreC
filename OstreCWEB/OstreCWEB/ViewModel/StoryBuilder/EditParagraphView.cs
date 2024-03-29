﻿using OstreCWEB.Data.Repository.Characters.Enums;
using OstreCWEB.Data.Repository.StoryModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.ViewModel.StoryBuilder
{
    public class EditParagraphView
    {
        public int Id { get; set; }

        public ParagraphType ParagraphType { get; set; }
        public string StageDescription { get; set; }
        public bool RestoreRest { get; set; }

        // Paragraph Fight properties
        public List<EnemyInParagraphView> ParagraphEnemies { get; set; }

        // ParagraphTest Properties
        [Display(Name = "Ability Score")]
        public AbilityScores AbilityScores { get; set; }
        [Display(Name = "Test Difficulty")]
        public TestDifficulty TestDifficulty { get; set; }

        // Db relations properties
        //public List<ParagraphItem> ParagraphItems { get; set; }
        public int FightPropId { get; set; }
        public int StoryId { get; set; }
    }
}
