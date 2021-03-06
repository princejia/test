﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.Models.Home
{
    public class SetCase : Model
    {
        [Key, StringLength(36)]
        public string Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(3000)]
        public string Content { get; set; }
        [StringLength(100)]
        public string PicName { get; set; }

        public DateTime Created { get; set; }
    }
}
