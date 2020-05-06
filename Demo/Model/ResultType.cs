using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class ResultType
    {
        public int ResultTypeid { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "类型名称")]
        public string Name { get; set; }

        public List<Result> Results { get; set; }//导航属性
    }
}
