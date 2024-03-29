﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please type the cover name!")]
        [DisplayName("Cover Type")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
