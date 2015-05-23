using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.Models.Home
{
    public class Message
    {
        [Key, StringLength(36)]
        public string Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }
        [Phone, StringLength(100)]
        public string Phone { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(3000)]
        public string Content { get; set; }

        public DateTime Created { get; set; }
    }
}
