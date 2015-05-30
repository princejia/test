using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.Models.Home
{
    public class Product : Model
    {
        [Key, StringLength(36)]
        public string Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "名称")]
        public string Title { get; set; }

        [StringLength(3000)]
        [Display(Name = "产品介绍")]
        public string Content { get; set; }
        [StringLength(100)]
        [Display(Name = "图片")]
        public string PicName { get; set; }
        [StringLength(100)]
        [Display(Name = "介绍图片")]
        public string IntroName { get; set; }
        [StringLength(100)]
        [Display(Name = "类型")]
        public string Type { get; set; }
        [Display(Name = "时间")]
        public DateTime Created { get; set; }
    }
}
