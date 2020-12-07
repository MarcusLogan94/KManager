using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Models
{
    public class OrderCreate
    {
        //takes in a string (CSV) 1,3,5,6. These are later parsed into the corresponding Int LIST
        [Required]
        public string ItemIDs { get; set; }

        [Required]
        public string ItemQuantities { get; set; }


    }
}
