using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBot.Models.DataBase
{
    [Table("WeaponType")]
    public class WeaponType
    {
        [Key]
        public int WeaponTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string WeaponTypeName { get; set; }= string.Empty;

        // Навигационное свойство
        public ICollection<Character> Characters { get; set; } = new List<Character>();
    }
}