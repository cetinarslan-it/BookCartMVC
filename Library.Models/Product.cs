using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1,10000)]
        [DisplayName("List Price")]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price of 1-50")]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price of 51-100")]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price of 100+")]
        public double Price100 { get; set; }

        [ValidateNever]
        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ValidateNever]
        [Display(Name = "Category")]
        public Category Category { get; set; }

        [Required]
        public int CoverTypeId { get; set; }

        [ValidateNever]
        [Display(Name = "Cover Type")]
        public CoverType CoverType { get; set; }
    }
}
