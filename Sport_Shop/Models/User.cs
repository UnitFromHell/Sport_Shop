using System;
using System.Collections.Generic;

namespace Sport_Shop.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int IdUser { get; set; }
        public string FirstNameUser { get; set; } = null!;
        public string SecondNameUser { get; set; } = null!;
        public string? MiddleNameUser { get; set; }
        public string LoginUser { get; set; } = null!;
        public string PasswordUser { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
