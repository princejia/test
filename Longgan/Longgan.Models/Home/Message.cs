using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.Models.Home
{
    public class Message : Model
    {
        [Key, StringLength(36)]
        public string Id { get; set; }
        [Required(ErrorMessage = "姓名不能为空"), StringLength(100)]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Required(ErrorMessage = "邮件不能为空"), EmailAddress(ErrorMessage = "请输入正确的邮箱地址"), StringLength(100)]
        [Display(Name = "邮件")]
        public string Email { get; set; }
        [Required(ErrorMessage = "电话不能为空"), System.ComponentModel.DataAnnotations.RegularExpression("\\d{8,11}", ErrorMessage = "请输入正确的电话号码"), StringLength(100)]
        [Display(Name = "电话")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "标题不能为空"), StringLength(100)]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required(ErrorMessage = "内容不能为空"), StringLength(3000)]
        [Display(Name = "内容")]
        public string Content { get; set; }

        public DateTime Created { get; set; }
    }
}
