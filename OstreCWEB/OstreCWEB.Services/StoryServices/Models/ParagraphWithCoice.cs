﻿using OstreCWEB.Data.Repository.StoryModels.Enums;

namespace OstreCWEB.Services.StoryServices.Models
{
    public class ParagraphWithCoice
    {
        public int Id { get; set; }
        public ParagraphType ParagraphType { get; set; }
        public string StageDescription { get; set; }
        public int AmountOfChoices { get; set; }

        public int ChoiceId { get; set; }
        public string DescriptionOfChoice { get; set; }
    }
}
