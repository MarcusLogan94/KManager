using KManager.Models;
using KManager.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KManager.WebMVC.Controllers
{
    [Authorize]
    public class ItemClassController : Controller
    {
        private ItemClassService CreateItemClassService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemClassService(userId);
            return service;
        }

        public ActionResult Index()
        {

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemClassService(userId);
            var model = service.GetItemClasses();

            return View(model);

        }

        //GET create page
        public ActionResult Create()
        {
            return View();
        }

        //POST create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemClassCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateItemClassService();

            if (service.CreateItemClass(model))
            {
                TempData["SaveResult"] = "Your Item Class was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Item Class could not be created.");

            return View(model);
        }

        //GET Details
        public ActionResult Details(int id)
        {
            var svc = CreateItemClassService();
            var model = svc.GetItemClassByID(id);

            return View(model);
        }

        //PUT edit itemclass
        public ActionResult Edit(int id)
        {
            var service = CreateItemClassService();
            var detail = service.GetItemClassByID(id);
            var model =
                new ItemClassEdit
                {
                    ItemID = detail.ItemID,
                    Name = detail.Name,
                    Price = detail.Price,
                    Description = detail.Description,
                    Category = detail.Category,
                    DoesExpire = detail.DoesExpire,
                    DaysToExpire = detail.DaysToExpire,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItemClassEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ItemID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateItemClassService();

            if (service.UpdateItemClass(model))
            {
                TempData["SaveResult"] = "Your item was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your item could not be updated.");
            return View(model);
        }

        //GET delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateItemClassService();
            var model = svc.GetItemClassByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateItemClassService();

            service.DeleteItemClass(id);

            TempData["SaveResult"] = "Your item class was deleted";

            return RedirectToAction("Index");
        }



    }
}