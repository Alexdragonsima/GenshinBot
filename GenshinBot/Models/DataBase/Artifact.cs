using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBot.Models.DataBase
{
    [Table("Artifacts")]
    public class Artifact
    {
        [Key]
        public int ArtifactID { get; set; }

        [Required]
        [MaxLength(200)]
        public string ArtifactName { get; set; }= string.Empty;

        [MaxLength(1000)]
        public string ArtifactSetBonus { get; set; }

        [MaxLength(100)]
        public string ArtifactBestForRole {  get; set; }

        // Навигационное свойство
        public virtual ICollection<CharacterArtifact> CharacterArtifacts { get; set; } = new List<CharacterArtifact>();
    }
}
