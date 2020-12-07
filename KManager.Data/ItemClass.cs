using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Data
{

    //defines item classes
    public class ItemClass
    {

        [Key]
        public int ItemID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public bool DoesExpire { get; set; }

        [Required]
        public int? DaysToExpire { get; set; }

        [Required]
        public DateTimeOffset AddedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }

    }
}
