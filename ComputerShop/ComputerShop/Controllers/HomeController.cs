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
        ComputerShopDbContext _db = new ComputerShopDbContext();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) // if the user is already logged in
            {
                return RedirectToAction("InStock", "Shop");
            }

            //IEnumerable<Equipment> equipments = _db.Equipments.Where(o => o.Status == Status.InStock).ToList();
            //ViewBag.Equipments = equipments;
            return View();
        }

        public ActionResult Contact()
        {
            //IEnumerable<Equipment> equipments = _db.Equipments.Where(o => o.Status == Status.InStock).ToList();
            //ViewBag.Equipments = equipments;
            return View();
        }

        [HttpGet]
        public ActionResult Buy(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public string Buy(Operation operation)
        {
            operation.Time = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            _db.Operations.Add(operation);
            // сохраняем в бд все изменения
            _db.SaveChanges();
            return "Спасибо," + operation.Destination + ", за покупку!";
        }
    }
}