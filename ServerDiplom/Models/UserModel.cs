using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerDiplom.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указан логин!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Не указан пароль!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
