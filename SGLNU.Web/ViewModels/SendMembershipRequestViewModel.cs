using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGLNU.Web.ViewModels
{
    public class SendMembershipRequestViewModel
    {
        [Required(ErrorMessage = "Поле ім'я є обов'язковим!")]
        [MinLength(4, ErrorMessage = "Поле ім'я повинне бути більшим!")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЇїЄєҐґ']{1,20}((\s+|-)[a-zA-Zа-яА-ЯІіЇїЄєҐґ']{1,20})*$",
            ErrorMessage = "Ім'я має містити тільки літери")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле прізвище є обов'язковим!")]
        [MinLength(4, ErrorMessage = "Поле прізвище повинне бути більшим!")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЇїЄєҐґ']{1,20}((\s+|-)[a-zA-Zа-яА-ЯІіЇїЄєҐґ']{1,20})*$",
            ErrorMessage = "Прізвище має містити тільки літери")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле електронна пошта є обов'язковим")]
        [EmailAddress(ErrorMessage = "Введене поле не є правильним для електронної пошти")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Введене поле не є правильним для електронної пошти")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле факультет є обов'язковим")]
        public string Faculty { get; set; }

        [Required(ErrorMessage = "Поле курс навчання є обов'язковим")]
        public string Course { get; set; }
    }
}
