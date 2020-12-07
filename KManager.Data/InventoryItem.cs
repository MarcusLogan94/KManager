using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Data
{
    public class InventoryItem
    {
        [Key]
        public int InventoryID { get; set; }

        [ForeignKey("ItemClass")]
        [Required]
        public int ItemID { get; set; }

        [Required]
        public ItemClass ItemClass { get; set; }

        [Required]
        public DateTimeOffset AddedUTC { get; set; }

        //exp date expected to be the day it was add + the expiration duration of the item if it has one. Can be  null for unexpiring (nonperishable) items
        [Required]
        public DateTimeOffset? ExpirationDate { get; set; }

        [Required]
        public bool Sold { get; set; }







    }
}
