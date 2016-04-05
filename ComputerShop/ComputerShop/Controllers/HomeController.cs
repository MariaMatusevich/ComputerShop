using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerShop.Models;
using ComputerShop.Infrastructure;

namespace ComputerShop.Controllers
{
    public class HomeController : Controller
    {
        ComputerShopRepository repo = new ComputerShopRepository();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) // if the user is already logged in
            {
                return RedirectToAction("InStock", "Shop");
            }

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Catalog()
        {
            var equipments = repo.GetEquipmentsByStatus(Status.InStock);
            ViewBag.Equipments = equipments;

            return View();
        }

        [HttpGet]
        public ActionResult Buy(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Buy(Operation operation)
        {
            var equipment = repo.GetEquipmentById(operation.EquipmentId);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            equipment.Status = Status.Sold;
            repo.ChangeEquipment(equipment);

            operation.Time = DateTime.Now;
            operation.Type = OperationType.Sold;
            operation.Id = Guid.NewGuid();
            // добавляем информацию о покупке в базу данных
            repo.AddOperation(operation);
            // сохраняем в бд все изменения
            repo.UpdateDatabase();
            //return "Спасибо," + operation.Destination + ", за покупку!";
            return RedirectToAction("Catalog", "Home");
        }
    }
}