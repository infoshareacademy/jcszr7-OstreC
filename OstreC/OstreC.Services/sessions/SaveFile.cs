﻿using Newtonsoft.Json;

namespace OstreC.Services
{ 
    public class SaveFile
    {
        public ParagraphType ActiveParagraphType { get; set; }
        public int ActiveParagraph { get; set; }
        public string NameOfStory { get; set; }

        

        [JsonIgnore]
        public Story CurrentStory { get; private set; }
        public SaveFile(ParagraphType activeParagraphType, int activeParagraph, string nameOfStory)
        {
            ActiveParagraphType = activeParagraphType;
            ActiveParagraph = activeParagraph;
            NameOfStory = nameOfStory;
            CurrentStory = LoadStory(NameOfStory);
        }
        private static Story LoadStory(string nameOfStory)
        {
            return JsonFile.DeserializeStory(nameOfStory);
        }
    }
}