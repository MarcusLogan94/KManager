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
    public class OrderController : Controller
    {
        private OrderService CreateOrderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new OrderService(userId);
            return service;
        }

        public ActionResult Index()
        {

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new OrderService(userId);
            var model = service.GetOrders();

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
        public ActionResult Create(OrderCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateOrderService();

            if (service.CreateOrder(model))
            {
                TempData["SaveResult"] = "Your order was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Order could not be created.");

            return View(model);
        }

        //GET Details
        public ActionResult Details(int id)
        {
            var svc = CreateOrderService();
            var model = svc.GetOrderByID(id);

            return View(model);
        }

        //PUT edit order
        public ActionResult Edit(int id)
        {
            var service = CreateOrderService();
            var detail = service.GetOrderByID(id);
            var model =
                new OrderEdit
                {
                    OrderID = detail.OrderID,
                    ItemIDs = detail.ItemIDs,
                    ItemQuantities = detail.ItemQuantities,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.OrderID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateOrderService();

            if (service.UpdateOrder(model))
            {
                TempData["SaveResult"] = "Your order was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your order could not be updated.");
            return View(model);
        }

        //GET delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateOrderService();
            var model = svc.GetOrderByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateOrderService();

            service.DeleteOrder(id);

            TempData["SaveResult"] = "Your order was deleted";

            return RedirectToAction("Index");
        }

    }
}