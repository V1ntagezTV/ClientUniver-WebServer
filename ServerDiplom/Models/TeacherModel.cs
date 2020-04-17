using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServerDiplom.Models
{
    public class TeacherModel
    {
        private string firName;
        private string lastName;
        private string middleName;
        

        [Key]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Пропущено поле!")]
        [MinLength(3)]
        public string FirstName
        {
            get => firName;
            set => firName = value.Substring(0, 1).ToUpper() + value.Substring(1);
        }
        [Required (ErrorMessage = "Пропущено поле!")]
        [MinLength(3)]
        public string LastName
        {
            get => lastName;
            set => lastName = value.Substring(0, 1).ToUpper() + value.Substring(1);
        }
        [Required (ErrorMessage = "Пропущено поле!")]
        [MinLength(3)]
        public string MiddleName
        {
            get => middleName;
            set => middleName = value.Substring(0, 1).ToUpper() + value.Substring(1);
        }
    }
}