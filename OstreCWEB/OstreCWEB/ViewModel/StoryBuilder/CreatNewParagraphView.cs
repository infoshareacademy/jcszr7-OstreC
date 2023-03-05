﻿using OstreCWEB.DomainModels.StoryModels.Enums;

namespace OstreCWEB.ViewModel.StoryBuilder
{
    public class CreatNewParagraphView
    {
        public ParagraphType ParagraphType { get; set; }
        public string StageDescription { get; set; }
        public bool RestoreRest { get; set; }

        //public int AmountOfItems { get; set; }
        //public int Id { get; set; }

        public int StoryId { get; set; }
    }
}
