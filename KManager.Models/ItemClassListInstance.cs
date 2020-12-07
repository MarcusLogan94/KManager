using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Models
{
    public class ItemClassListInstance
    {

        public int ItemID { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }
        public bool DoesExpire { get; set; }

        public int? DaysToExpire { get; set; }

        public DateTimeOffset AddedUTC { get; set; }




    }
}
