﻿using OstreCWEB.Data.Repository.StoryModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.ViewModel.StoryBuilder
{
    public class ParagraphWithCoiceView
    {
        public int Id { get; set; }

        [Display(Name = "Paragraph Type")]
        public ParagraphType ParagraphType { get; set; }

        [Display(Name = "Stage Description")]
        public string StageDescription { get; set; }

        [Display(Name = "Amount Of Choices")]
        public int AmountOfChoices { get; set; }

        public int ChoiceId { get; set; }
        public string DescriptionOfChoice { get; set; }
    }
}
