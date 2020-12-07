using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Models
{
    public class OrderListInstance
    {

        public int OrderID { get; set; }

        public string ItemIDs { get; set; }

        public string ItemQuantities { get; set; }

        public double TotalPrice { get; set; }

        public DateTimeOffset AddedUTC { get; set; }



    }
}
