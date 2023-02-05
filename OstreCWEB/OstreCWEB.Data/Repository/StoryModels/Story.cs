﻿using OstreCWEB.Data.Repository.Identity;

namespace OstreCWEB.Data.Repository.StoryModels
{
    public class Story
    {
        // General
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<Paragraph> Paragraphs { get; set; } = new List<Paragraph>();
        public int FirstParagraphId { get; set; }

        // DB
        public string? UserId { get; set; }
        public User? User { get; set; }

        public int GetAmountOfParagraphs()
        {
            return Paragraphs.Count();
        }
    }
}
