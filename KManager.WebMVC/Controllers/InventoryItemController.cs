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
    public class InventoryItemController : Controller
    {
        private InventoryItemService CreateInventoryItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new InventoryItemService(userId);
            return service;
        }

        public ActionResult Index()
        {

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new InventoryItemService(userId);
            var model = service.GetInventoryItems();

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
        public ActionResult Create(InventoryItemCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateInventoryItemService();

            if (service.CreateInventoryItem(model))
            {
                TempData["SaveResult"] = "Your Inventory Item was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Inventory Item could not be created.");

            return View(model);
        }

        //GET Details
        public ActionResult Details(int id)
        {
            var svc = CreateInventoryItemService();
            var model = svc.GetInventoryItemByID(id);

            return View(model);
        }

        //PUT edit itemclass
        public ActionResult Edit(int id)
        {
            var service = CreateInventoryItemService();
            var detail = service.GetInventoryItemByID(id);
            var model =
                new InventoryItemEdit
                {
                    InventoryID = detail.InventoryID,
                    ItemID = detail.ItemID,
                    Sold = detail.Sold,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InventoryItemEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.InventoryID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateInventoryItemService();

            if (service.UpdateInventoryItem(model))
            {
                TempData["SaveResult"] = "Your inventoryItem was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your inventoryItem could not be updated.");
            return View(model);
        }

        //GET delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateInventoryItemService();
            var model = svc.GetInventoryItemByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateInventoryItemService();

            service.DeleteInventoryItem(id);

            TempData["SaveResult"] = "Your inventory item was deleted";

            return RedirectToAction("Index");
        }


    }
}