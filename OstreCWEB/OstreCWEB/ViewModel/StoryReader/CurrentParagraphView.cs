﻿using OstreCWEB.Data.Repository.StoryModels.Enums;

namespace OstreCWEB.ViewModel.StoryReader
{
    public class CurrentParagraphView
    {
        public ParagraphType ParagraphType { get; set; }
        public string StageDescription { get; set; }
        public List<CurrentChoicesView> Choices { get; set; }
    }
}
