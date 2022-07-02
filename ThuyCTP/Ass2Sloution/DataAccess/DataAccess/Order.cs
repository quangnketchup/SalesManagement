﻿using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequireDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public int Status { get; set; }

        public virtual Member Member { get; set; } = null!;
    }
}
