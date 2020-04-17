using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerDiplom.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [MinLength(6, ErrorMessage = "Не достаточно длинный пароль")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Не достаточно длинный пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
