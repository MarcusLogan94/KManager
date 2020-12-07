using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Models
{
    public class InventoryItemCreate
    {

        [Required]
        public int ItemID { get; set; }

    }
}
