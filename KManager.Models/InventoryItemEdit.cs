using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Models
{
    public class InventoryItemEdit
    {
        public int InventoryID { get; set; }

        public int ItemID { get; set; }
     
        public bool Sold { get; set; }

    }
}
