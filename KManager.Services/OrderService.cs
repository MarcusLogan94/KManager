using KManager.Data;
using KManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Services
{
    public class OrderService
    {
        private readonly Guid _userId;

        public OrderService(Guid userId)
        {
            _userId = userId;
        }

        public string IntListToString(List<int> list)
        {
            //initialize the char[]. We need 3 char spaces for each item in the list, as each int shows up, plus a comma plus a space. Technically we dont need the last two but who cares
            char[] ListToCharArray = new char[list.Count + (list.Count) + (list.Count)];

            for (int i = 0; i < list.Count; i++)
            {
                ListToCharArray.Append((char)list[i]);
                if (i != (list.Count - 1))
                {
                    ListToCharArray.Append(',');
                    ListToCharArray.Append(' ');
                }
            }

            return ListToCharArray.ToString();
        }

        public int CharToInt(char c)
        {
            return (int)(c - '0');
        }

        public List<int> StringToIntList(string list)
        {
            List<int> convertedList = new List<int>();

            char[] listToCharArray = list.ToCharArray();

            for(int i = 0; i < listToCharArray.Length; i++)
            {
                if (listToCharArray[i] != ',' && listToCharArray[i] != '\0')
                {
                    int number = Int32.Parse(listToCharArray[i].ToString());
                    convertedList.Add(number);
                }
            }
  
            return convertedList;

        }

        public bool CreateOrder(OrderCreate model)
        {

            List<int> IDList = StringToIntList(model.ItemIDs);

            List<int> QuantitiesList = StringToIntList(model.ItemQuantities);

            //now we do the proper conversion for pricecalculation based on our new lists
            using (var ctx = new ApplicationDbContext())
            {

                double priceCalculation = 0;

                for (int i = 0; i < IDList.Count; i++)
                {
                    int id = IDList[i];
                    int quantity = QuantitiesList[i];

                    var ReferredItem =
                         ctx
                             .ItemClasses
                             .Single(e => e.ItemID == id);

                    //new InventoryItemService InvItemService
                    //InvItemService.MarkAsSold(ReferredItem)

                    priceCalculation = priceCalculation + ((double)ReferredItem.Price * (double)quantity);

                }


                var entity =
                    new Order()
                    {
                        ItemIDs = model.ItemIDs,
                        ItemQuantities = model.ItemQuantities,
                        TotalPrice = priceCalculation,
                        AddedUTC = DateTimeOffset.Now
                    };

                ctx.Orders.Add(entity);
                return ctx.SaveChanges() == 1;

            }
        }

        

        /*
        public OrderListInstanceCONVERTED ConvertToFormat(OrderListInstanceUNCONVERTED unconvertedOrder)
        {
            OrderListInstanceCONVERTED convertedOrder = new OrderListInstanceCONVERTED();

            convertedOrder.OrderID = unconvertedOrder.OrderID;
            convertedOrder.ItemIDs = IntListToString(unconvertedOrder.ItemIDs);
            convertedOrder.ItemQuantities = IntListToString(unconvertedOrder.ItemQuantities);
            convertedOrder.TotalPrice = unconvertedOrder.TotalPrice;
            convertedOrder.AddedUTC = unconvertedOrder.AddedUTC;

            return convertedOrder;
        
        }
        */

        public IEnumerable<OrderListInstance> GetOrders()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Orders
                        .Where(e => e.OrderID != null)
                        .Select(
                            e =>
                                new OrderListInstance
                                {
                                    OrderID = e.OrderID,
                                    ItemIDs = e.ItemIDs,
                                    ItemQuantities = e.ItemQuantities,
                                    TotalPrice = e.TotalPrice,
                                    AddedUTC = e.AddedUTC,

                                }
                        );

                var result = query.ToArray();
                return result;

                /*
                var resultConverted = new OrderListInstanceCONVERTED[result.Length];

                for (int i = 0; i < result.Length; i++)
                {
                    resultConverted[i] = ConvertToFormat(result[i]);
                }


                return resultConverted;
                */
            }
        }

        public OrderDetail GetOrderByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Orders
                        .Single(e => e.OrderID == id);
                return
                    new OrderDetail
                    {
                        OrderID = entity.OrderID,
                        ItemIDs = entity.ItemIDs,
                        ItemQuantities = entity.ItemQuantities,
                        TotalPrice = entity.TotalPrice,
                        AddedUTC = entity.AddedUTC,
                        ModifiedUTC = entity.ModifiedUTC
                    };
            }
        }

        public bool UpdateOrder(OrderEdit model)
        {

            List<int> IDList = StringToIntList(model.ItemIDs);

            List<int> QuantitiesList = StringToIntList(model.ItemQuantities);


            using (var ctx = new ApplicationDbContext())
            {
                double priceCalculation = 0;

                if (IDList.Count > 0)
                {
                    for (int i = 0; i < IDList.Count; i++)
                    {
                        int id = IDList[i];
                        int quantity = QuantitiesList[i];

                        var ReferredItem =
                                ctx
                                    .ItemClasses
                                    .Single(e => e.ItemID == id);


                        priceCalculation = priceCalculation + ((double)ReferredItem.Price * (double)quantity);
                        
                    }
                }
                    

                var entity =
                    ctx
                        .Orders
                        .Single(e => e.OrderID == model.OrderID);

                entity.ItemIDs = model.ItemIDs;
                entity.ItemQuantities = model.ItemQuantities;
                entity.TotalPrice = priceCalculation;
                entity.ModifiedUTC = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteOrder(int orderID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Orders
                        .Single(e => e.OrderID == orderID);

                ctx.Orders.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }



    }
}
