﻿using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Identity;
using OstreCWEB.Data.Repository.StoryModels;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.Data.DataBase.ManyToMany
{
    //It will be replaced by story reader mclasses most likely.
    public class UserParagraph
    {
        [Key]
        public int UserParagraphId { get; set; } 
        //Many to many
        public string UserId { get; set; }
        public User User { get; set; }
        public int ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
        //One to one 
        public int? CharacterId { get; set; } 
        public Character? Character { get; set; }
        public bool ActiveGame { get; set; }
    }
}
