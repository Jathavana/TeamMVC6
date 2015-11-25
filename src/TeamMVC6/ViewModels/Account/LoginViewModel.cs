using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMVC6.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(9)]
        [RegularExpression(@"^A00\d{6}$", ErrorMessage = "Proper format is: A00######")]
        [Display(Name = "Student Number")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
