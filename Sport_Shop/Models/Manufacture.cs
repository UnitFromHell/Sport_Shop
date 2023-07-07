using System;
using System.Collections.Generic;

namespace Sport_Shop.Models
{
    public partial class Manufacture
    {
        public Manufacture()
        {
            Products = new HashSet<Product>();
        }

        public int IdManufacture { get; set; }
        public string Country { get; set; } = null!;
        public string CompanyName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
