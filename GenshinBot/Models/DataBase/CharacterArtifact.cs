using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBot.Models.DataBase
{
    [Table("CharacterArtifacts")]
    public class CharacterArtifact
    {
        [Key]
        public int CharacterArtifactId { get; set; }

        [Required]
        [ForeignKey("Character")]
        public int CharacterID { get; set; }

        [Required]
        [ForeignKey("Artifact")]
        public int ArtifactID {  get; set; }

        public int CharacterArtifactPriority { get; set; } = 1;

        [MaxLength(500)]
        public string CharacterArtifactMainStats {  get; set; }

        [MaxLength(500)]
        public string CharacterArtifactSubStatsPriority {  get; set; }

        // Навигационные свойства
        public virtual Character Character { get; set; }
        public virtual Artifact Artifact { get; set; }
    }
}
