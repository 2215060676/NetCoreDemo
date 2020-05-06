using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class Result
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
        [Display(Name = "创建时间")]
        public DateTime Create { get; set; }
        [Display(Name = "类型")]
        public int TypeId { get; set; }

        [Display(Name = "类型")]
        public ResultType type { get; set; }//导航属性

        public string Password { get; set; }
        [Display(Name = "附件")]
        public string Attachment { get; set; }
    }
}
