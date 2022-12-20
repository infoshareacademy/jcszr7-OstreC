﻿namespace OstreCWEB.Data.Repository.StoryModels.Properties
{
    public class Choice
    {
        // General
        public int Id { get; set; }

        public string ChoiceText { get; set; }
        public int NextParagraphId { get; set; }

        // Db relations properties
        public int ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
    }
}
