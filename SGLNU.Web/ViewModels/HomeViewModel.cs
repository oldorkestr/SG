using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuLNU.Web.ViewModels
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "Поле ім'я є обов'язковим!")]
        [MinLength(4, ErrorMessage = "Поле ім'я та прізвище повинне бути більшим!")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЇїЄєҐґ']{1,20}((\s+|-)[a-zA-Zа-яА-ЯІіЇїЄєҐґ']{1,20})*$",
            ErrorMessage = "Ім'я та прізвище має містити тільки літери")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле електронна пошта є обов'язковим")]
        [EmailAddress(ErrorMessage = "Введене поле не є правильним для електронної пошти")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Введене поле не є правильним для електронної пошти")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле текст є обов'язковим")]
        [MinLength(10, ErrorMessage = "Поле текст повинне містити більше символів!")]
        public string FeedBackDescription { get; set; }
        public bool IsUserLogedIn { get; set; } 
        public string UserEmail { get; set; }
    }
}
