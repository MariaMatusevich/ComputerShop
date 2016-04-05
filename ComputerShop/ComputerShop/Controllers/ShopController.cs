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
        ComputerShopRepository repo = new ComputerShopRepository();
        ApplicationDbContext _udb = new ApplicationDbContext();
        ApplicationUser CurrentUser = null;


        private void RefreshUserName()
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            CurrentUser = _udb.Users.Where(o => o.Id == userId).FirstOrDefault();
        }

        public ActionResult InStock()
        {
            RefreshUserName();

            var equipments = repo.GetEquipmentsByStatus(Status.InStock);
            ViewBag.Equipments = equipments;

            ViewBag.UserName = CurrentUser.Name;

            return View();
        }

        public ActionResult Sold()
        {
            RefreshUserName();

            var equipments = repo.GetEquipmentsByStatus(Status.Sold);
            ViewBag.Equipments = equipments;

            ViewBag.UserName = CurrentUser.Name;

            return View();
        }

        [HttpGet]
        public ActionResult Sell(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Sell(Operation operation)
        {
            var equipment = repo.GetEquipmentById(operation.EquipmentId);
            if(equipment == null)
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
            return RedirectToAction("InStock", "Shop");
        }

        [HttpPost]
        public ActionResult AddEquipment(AddEquipmentModel equipmentModel)
        {
            var equipment = new Equipment(equipmentModel.Type, equipmentModel.Company, equipmentModel.Model, Status.InStock, equipmentModel.Price);
            repo.AddEquipment(equipment);

            var operation = new Operation(Guid.NewGuid(), OperationType.ToStock, equipmentModel.Destination, equipment.Id, DateTime.Now);
            repo.AddOperation(operation);

            return RedirectToAction("InStock", "Shop");
        }
    }
}