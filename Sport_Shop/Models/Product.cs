using System;
using System.Collections.Generic;

namespace Sport_Shop.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int IdProduct { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal ProductCost { get; set; }
        public int ManufactureId { get; set; }
        public int TypeProductId { get; set; }

        public string Image { get; set; }

        public virtual Manufacture Manufacture { get; set; } = null!;
        public virtual TypeProduct TypeProduct { get; set; } = null!;

        
        public virtual ICollection<Order> Orders { get; set; }
    }
}
