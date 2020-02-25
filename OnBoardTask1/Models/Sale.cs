using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnBoardTask1.Models
{
    public class Sale
    {
        public long SaleID { get; set; }
        public long ProductID { get; set; }
        public long CustomerID { get; set; }

        public long StoreID { get; set; }

        public DateTime? DateSold { get; set; }

    }
}