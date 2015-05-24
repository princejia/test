using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.Models.Home
{
    public class New : Model
    {
        [Key, StringLength(36)]
        public string Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name="名称")]
        public string Title { get; set; }

        [Required, StringLength(3000)]
        [Display(Name = "内容")]
        public string Content { get; set; }

        public int Hot { get; set; }

        public DateTime Created { get; set; }
    }
}
