using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBot.Models.DataBase
{
    [Table("Elements")]
    public class Element
    {
        [Key]
        public int ElementID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ElementName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string ElementColor { get; set; }

        [MaxLength(500)]
        public string ElementDescription {  get; set; }

        // Навигационное свойство
        public ICollection<Character> Characters { get; set; } = new List<Character>();
    }
}
