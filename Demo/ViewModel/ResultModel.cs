using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ViewModel
{
    public class ResultModel
    {
        public int id { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "姓名")]
        public string strName { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "成果概述")]
        public string Discription { get; set; }

        [Display(Name = "类型名称")]
        public int TypeId { get; set; }
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "附件")]
        public IFormFile Attachment { get; set; }//因为要上传的是文件，所以写一个接口
    }
}
