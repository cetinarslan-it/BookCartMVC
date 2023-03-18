using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class ShoppingCartVM
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }

    }
}
