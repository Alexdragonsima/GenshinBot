using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBot.Models.DataBase
{
    [Table("Characters")]
    public class Character
    {
        [Key]
        public int CharacterID { get; set; }

        [Required]
        [MaxLength(100)]
        public string CharacterName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string CharacterTitle { get; set; }

        [Required]
        [ForeignKey("Element")]
        public int ElementID { get; set; }

        [Required]
        [ForeignKey("WeaponType")]
        public int WeaponTypeID { get; set; }

        [Required]
        [ForeignKey("Region")]
        public int RegionID { get; set; }

        [Required]
        [ForeignKey("Rarity")]
        public int RarityID { get; set; }

        [MaxLength(2000)]
        public string CharacterDescription { get; set; }

        [MaxLength(50)]
        public string CharacterBirthDate { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public bool IsRelised { get; set; } = true;

        [MaxLength(20)]
        public string ReleaseVersion { get; set; }

        // Навигационные свойства
        public virtual Element Element { get; set; }
        public virtual WeaponType WeaponType { get; set; }
        public virtual Region Region { get; set; }
        public virtual Rarity Rarity { get; set; }

        public virtual ICollection<CharacterTeam> CharacterTeams { get; set; } = new List<CharacterTeam>();
        public virtual ICollection<CharacterArtifact> CharacterArtifacts { get; set; } = new List<CharacterArtifact>();
    }

    [Table("Rarities")]
    public class Rarity
    {
        [Key]
        public int RarityID { get; set; }

        [Required]
        [Range(1,5)]
        public int Stars {  get; set; }

        [MaxLength(20)]
        public string RarityColor {  get; set; }

        // Навигационное свойство
        public ICollection<Character> Characters { get; set; }= new List<Character>();
    }
}
