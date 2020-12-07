using KManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Models
{
    public class InventoryItemListInstance
    {

        public int InventoryID { get; set; }

        public int ItemID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public DateTimeOffset AddedUTC { get; set; }

        public bool DoesExpire { get; set; }

        //exp date expected to be the day it was add + the expiration duration of the item if it has one. Can be  null for unexpiring (nonperishable) items
        public DateTimeOffset? ExpirationDate { get; set; }

        public bool Sold { get; set; }




    }
}
