using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBot.Models.DataBase
{
    [Table("CharacterTeams")]
    public class CharacterTeam
    {
        [Key]
        public int CharacterTeamID { get; set; }

        [Required]
        [ForeignKey("Character")]
        public int CharacterID { get; set; }

        [Required]
        [ForeignKey("Team")]
        public int TeamID { get; set; }

        [MaxLength(100)]
        public string RoleInTeam { get; set; }

        public int CharacterTeamPriority { get; set; } = 1;

        // Навигационные свойства
        public virtual Character Character { get; set; }
        public virtual Team Team { get; set; }
    }
}
