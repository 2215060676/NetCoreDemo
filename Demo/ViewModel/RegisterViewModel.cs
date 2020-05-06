using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ViewModel
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }


        [Required]
        [StringLength(16, ErrorMessage = "{0} 长度必须大于{2}位数小于{1}位", MinimumLength = 6)]
        [Display(Name = "密码")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }
    }
}
