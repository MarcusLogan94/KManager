using KManager.Data;
using KManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Services
{
    public class InventoryItemService
    {
        private readonly Guid _userId;

        public InventoryItemService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateInventoryItem(InventoryItemCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var ReferredItem =
                         ctx
                             .ItemClasses
                             .Single(e => e.ItemID == model.ItemID);

                DateTimeOffset? expiration = null;

                if (ReferredItem.DoesExpire)
                {
                    expiration = DateTimeOffset.Now.AddDays((double)ReferredItem.DaysToExpire);
                }


                var entity =
                    new InventoryItem()
                    {
                        ItemID = model.ItemID,
                        ItemClass = ReferredItem,
                        AddedUTC = DateTimeOffset.Now,
                        ExpirationDate = expiration,
                        Sold = false,
                    };

                ctx.InventoryItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<InventoryItemListInstance> GetInventoryItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .InventoryItems
                        .Where(e => e.InventoryID != null)
                        .Select(
                            e =>
                                new InventoryItemListInstance
                                {
                                    InventoryID = e.InventoryID,
                                    ItemID = e.ItemID,
                                    Name = e.ItemClass.Name,
                                    Price = e.ItemClass.Price,
                                    AddedUTC = e.AddedUTC,
                                    DoesExpire = e.ItemClass.DoesExpire,
                                    ExpirationDate = e.ExpirationDate,
                                    Sold = e.Sold

                                }
                        );

                return query.ToArray();
            }
        }

        public InventoryItemDetail GetInventoryItemByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity =
                    ctx
                        .InventoryItems
                        .Single(e => e.InventoryID == id);

                var ReferredItem =
                        ctx
                            .ItemClasses
                            .Single(e => e.ItemID == entity.ItemID);

                return
                    new InventoryItemDetail
                    {
                        InventoryID = id,
                        ItemID = entity.ItemID,
                        Name = ReferredItem.Name,
                        Price = ReferredItem.Price,
                        AddedUTC = entity.AddedUTC,
                        DoesExpire = ReferredItem.DoesExpire,
                        ExpirationDate = entity.ExpirationDate,
                        Sold = entity.Sold
                    };
            }
        }

        public bool UpdateInventoryItem(InventoryItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var ReferredItem =
                         ctx
                             .ItemClasses
                             .Single(e => e.ItemID == model.ItemID);

                DateTimeOffset? expiration = null;

                if (ReferredItem.DoesExpire)
                {
                    expiration = DateTimeOffset.Now.AddDays((double)ReferredItem.DaysToExpire);
                }


                var entity =
                    ctx
                        .InventoryItems
                        .Single(e => e.InventoryID == model.InventoryID);

                entity.ItemID = model.ItemID;
                entity.ItemClass = ReferredItem;
                entity.ExpirationDate = expiration;
                entity.Sold = model.Sold;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteInventoryItem(int inventoryID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .InventoryItems
                        .Single(e => e.InventoryID == inventoryID);

                ctx.InventoryItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }




    }
}
