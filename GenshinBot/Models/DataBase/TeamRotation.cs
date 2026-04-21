using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBot.Models.DataBase
{
    [Table("TeamRotations")]
    public class TeamRotation
    {
        [Key]
        public int RotationID { get; set; }

        [Required]
        [ForeignKey("Team")]
        public int TeamID {  get; set; }

        [Required]
        public int RotationOrder {  get; set; }

        [Required]
        [ForeignKey("Character")]
        public int CharaterID {  get; set; }

        [MaxLength(500)]
        public string ActionDescription {  get; set; }= string.Empty;

        [MaxLength(500)]
        public string Notes {  get; set; }

        // Навигационные свойства
        public virtual Team Team { get; set; }
        public virtual Character Character { get; set; }
    }
}
