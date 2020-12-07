using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Models
{
    public class InventoryItemDetail
    {

        public int InventoryID { get; set; }

        public int ItemID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public DateTimeOffset AddedUTC { get; set; }

        public bool DoesExpire { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }

        public bool Sold { get; set; }


    }
}
