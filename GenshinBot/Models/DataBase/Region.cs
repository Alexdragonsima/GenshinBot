using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBot.Models.DataBase
{
    [Table("Regions")]
    public class Region
    {
        [Key]
        public int RegionID { get; set; }

        [Required]
        [MaxLength(50)]
        public string RegionName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string RegionDescription { get; set; }

        // Навигационное свойство
        public ICollection<Character> Characters { get; set; } = new List<Character>();
    }
}
