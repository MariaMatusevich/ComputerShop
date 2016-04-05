using ComputerShop.Infrastructure;
using ComputerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComputerShop.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        // GET: Shop
        ComputerShopDbContext _db = new ComputerShopDbContext();
        ApplicationDbContext _udb = new ApplicationDbContext();
        ApplicationUser CurrentUser = null;


        public ShopController()
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            CurrentUser = _udb.Users.Where(o => o.Id == userId).FirstOrDefault();
        }

        public ActionResult InStock()
        {
            IEnumerable<Equipment> equipments = _db.Equipments.Where(o => o.Status == Status.InStock).ToList();
            ViewBag.Equipments = equipments;

            ViewBag.UserName = CurrentUser.Name;

            return View();
        }

        public ActionResult Sold()
        {
            IEnumerable<Equipment> equipments = _db.Equipments.Where(o => o.Status == Status.Sold).ToList();
            ViewBag.Equipments = equipments;

            ViewBag.UserName = CurrentUser.Name;

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