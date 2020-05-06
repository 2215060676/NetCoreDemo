using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ViewModel
{
    public class loginViewModel
    {


        public int Id { get; set; }

        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string UserName { get; set; }


        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string PassWord { get; set; }


        [Display(Name = "记住登录状态")]
        public bool IsRemember { get; set; }
    }
}
