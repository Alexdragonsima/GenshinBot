using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBot.Models.DataBase
{
    [Table("Teams")]
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        [Required]
        [MaxLength(200)]
        public string TeamName {  get; set; }=string.Empty;

        [MaxLength(1000)]
        public string TeamDescription { get; set; }

        [MaxLength(100)]
        public string TeamPlayStyle {  get; set; }

        [Range(1,5)]
        public int TeamDifficulty {  get; set; }

        [MaxLength(200)]
        public string TeamEnergyRechargeRequirement {  get; set; }

        // Навигационное свойство
        public virtual ICollection<CharacterTeam> CharacterTeams { get; set; } = new List<CharacterTeam>();
        public virtual ICollection<TeamRotation> Rotations { get; set; } = new List<TeamRotation>();
    }
}
