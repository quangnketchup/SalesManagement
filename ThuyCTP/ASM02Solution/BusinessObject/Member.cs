﻿using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess
{
    public partial class Member
    {
        public Member()
        {
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string Email { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Status { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
