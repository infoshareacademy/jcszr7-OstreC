using OstreCWEB.Data.Repository.Characters.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Data.Repository.Characters.CoreClasses
{
    public class CharacterSkills
    {
        [Key]
        public int SkillsId { get; set; }
        public string SkillName { get; set; }
        public Statistics StatisticForTest { get; set; }
    }
}