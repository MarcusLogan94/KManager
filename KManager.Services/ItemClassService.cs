using KManager.Data;
using KManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KManager.Services
{
    public class ItemClassService
    {
        private readonly Guid _userId;

        public ItemClassService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateItemClass(ItemClassCreate model)
        {
            var entity =
                new ItemClass()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Category = model.Category,
                    DoesExpire = model.DoesExpire,
                    DaysToExpire = model.DaysToExpire,
                    AddedUTC = DateTimeOffset.Now,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ItemClasses.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ItemClassListInstance> GetItemClasses()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ItemClasses
                        .Where(e => e.ItemID != null)
                        .Select(
                            e =>
                                new ItemClassListInstance
                                {
                                    ItemID = e.ItemID,
                                    Name = e.Name,
                                    Price = e.Price,
                                    Description = e.Description,
                                    Category = e.Category,
                                    DoesExpire = e.DoesExpire,
                                    DaysToExpire = e.DaysToExpire,
                                    AddedUTC = e.AddedUTC,

                                }
                        );

                return query.ToArray();
            }
        }
        public ItemClassDetail GetItemClassByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ItemClasses
                        .Single(e => e.ItemID == id);
                return
                    new ItemClassDetail
                    {
                        ItemID= entity.ItemID,
                        Name = entity.Name,
                        Price = entity.Price,
                        Description = entity.Description,
                        Category = entity.Category,
                        DoesExpire = entity.DoesExpire,
                        DaysToExpire = entity.DaysToExpire,
                        AddedUTC = entity.AddedUTC,
                        ModifiedUTC = entity.ModifiedUTC,
                    };
            }
        }

        public bool UpdateItemClass(ItemClassEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ItemClasses
                        .Single(e => e.ItemID == model.ItemID);

                entity.ItemID = model.ItemID;
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Description = model.Description;
                entity.Category = model.Category;
                entity.DoesExpire = model.DoesExpire;
                entity.DaysToExpire = model.DaysToExpire;
                entity.ModifiedUTC = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItemClass(int itemID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ItemClasses
                        .Single(e => e.ItemID == itemID);

                ctx.ItemClasses.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }






    }
}
