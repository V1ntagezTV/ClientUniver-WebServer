using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServerDiplom.Models
{
    public class TeacherModel
    {
        [Key]
        public int TeacherId { get; set; }
        [Required (ErrorMessage ="Пропущено поле!")]
        public string FirstName { get; set; }
        [Required (ErrorMessage = "Пропущено поле!")]
        public string LastName { get; set; }
        [Required (ErrorMessage = "Пропущено поле!")]
        public string MiddleName { get; set; }
    }
}