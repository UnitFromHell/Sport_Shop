using System;
using System.Collections.Generic;

namespace Sport_Shop.Models
{
    public partial class TypeProduct
    {
        public TypeProduct()
        {
            Products = new HashSet<Product>();
        }

        public int IdTypeProduct { get; set; }
        public string NameType { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
