using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Data
{
    public class Order
    {
        [Required]
        public int OrderID { get; set; }

        //notice: not foreign keys. Theres a way to do FKs in a group like this, but i plan on handling the logic at a higher level rather than store it in the table
        //you list the IDs of the items (done easily through the creation process)
        //you list the quantities (easy)
        //it stores the itemIDs and quantities in separate lists, and then if you want to do stuff with that information itll compare these lists (index0 of items, index0 of quantities) etc.
        
        //after some issues with LINQ, the easy fix was changing things from lists of ints to strings and then just doing the logic on the string.
        //these must be entered at CSVs


        // IDs:         [1,3,5]
        // Quantities:  [4,3,1]

        // IDs:         "1,3,5"
        // Quants:      "4,3,1"


        // BestDefense: true
        // BestOffense: false
        // MVP:         true
        // MostSteals   true


        // string distinctions = "";
        // for(each distinction){
        //   if(distinction==true){
        //       distinctions = distinctions + distictionText
        //       if(not last distinction){
        //           distinctions = distinctions + ", "

        // disctions == "BestDefense, MVP, MostSteals"
        //
        /// <summary>
        /// PlayerID: 1
        /// First Name: LeBron
        /// Last Name: James
        /// blahblahblah
        /// height: amillion
        /// 
        /// distinctions: "BestDefense, MVP, MostSteals"
        /// 
        /// 
        /// 
        /// </summary>



        [Required]
        public string ItemIDs { get; set; }

        [Required]
        public string ItemQuantities { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public DateTimeOffset AddedUTC { get; set; }


        public DateTimeOffset? ModifiedUTC { get; set; }





    }
}
