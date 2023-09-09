using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.Domain.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно быть не больше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должно быть не меньше 3 символов")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
