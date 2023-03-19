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
        public string Title { get; set; } = LoremNET.Lorem.Words(1) + " " + LoremNET.Lorem.Words(1) + " " + LoremNET.Lorem.Words(1);
        public string Description { get; set; } = LoremNET.Lorem.Paragraph(5, 16, 4, 12);

        [Required]
        public string ISBN { get; set; } = LoremNET.Lorem.Number(1000, 2000).ToString()+" - "+ LoremNET.Lorem.Number(100000, 200000).ToString();

        [Required]
        public string Author { get; set; } = LoremNET.Lorem.Words(1) + " " + LoremNET.Lorem.Words(1);

        [Required]
        [Range(1, 10000)]
        [DisplayName("List Price")]
        public double ListPrice { get; set; } = LoremNET.Lorem.Number(60, 70);

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price of 1-50")]
        public double Price { get; set; } = LoremNET.Lorem.Number(50, 60);

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price of 51-100")]
        public double Price50 { get; set; } = LoremNET.Lorem.Number(40, 50);

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price of 100+")]
        public double Price100 { get; set; } = LoremNET.Lorem.Number(30, 40);

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
