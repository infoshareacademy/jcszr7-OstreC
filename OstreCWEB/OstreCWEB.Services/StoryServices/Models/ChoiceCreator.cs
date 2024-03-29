﻿using OstreCWEB.Data.Repository.StoryModels;

namespace OstreCWEB.Services.StoryServices.Models
{
    public class ChoiceCreator
    {
        public int Id { get; set; }

        public string ChoiceText { get; set; }
        public bool ChangePlaces { get; set; }

        public int ParagraphId { get; set; }
        public Paragraph PreviousParagraph { get; set; }

        public int NextParagraphId { get; set; }
        public Paragraph NextParagraph { get; set; }

        public int StoryId { get; set; }
    }
}
