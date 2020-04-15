using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServerDiplom.Models
{
    public class GroupModel
    {
        [Key]
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Пропущено поле!")]
        public int Cours { get; set; }
        [Required(ErrorMessage = "Пропущено поле!")]
        public string Faculty { get; set; }
        [Required(ErrorMessage = "Пропущено поле!")]
        public string TeachProfile { get; set; }
    }
}