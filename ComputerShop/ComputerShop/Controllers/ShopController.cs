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


        private void RefreshData()
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            CurrentUser = _udb.Users.Where(o => o.Id == userId).FirstOrDefault();

            ViewBag.UserName = CurrentUser.Name;
            ViewBag.PurchaseRequisitionCount = repo.GetPurchaseRequisitionCount();
        }

        public ActionResult Index()
        {
            return RedirectToAction("InStock", "Shop");
        }

        public ActionResult InStock()
        {
            var equipments = repo.GetEquipmentsByStatus(Status.InStock);
            ViewBag.Equipments = equipments;


            RefreshData();
            return View();
        }

        public ActionResult Sold()
        {
            var equipments = repo.GetEquipmentsByStatus(Status.Sold);
            ViewBag.Equipments = equipments;

            RefreshData();
            return View();
        }

        [HttpGet]
        public ActionResult Sell(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public JsonResult Sell(Operation operation)
        {
            var equipment = repo.GetEquipmentById(operation.EquipmentId);
            if(equipment == null)
            {
                return Json(new JsonResponse(JsonResponseType.Error, "Такого товара в базе нету. Обновите, пожалуйста, страницу."));
            }
            equipment.Status = Status.Sold;
            repo.ChangeEquipment(equipment);

            operation.Id = Guid.NewGuid();
            operation.Time = DateTime.Now;
            operation.Type = OperationType.Sold;
            operation.Price = equipment.Price;
            
            // добавляем информацию о покупке в базу данных
            repo.AddOperation(operation);
            // сохраняем в бд все изменения
            repo.UpdateDatabase();
            //return "Спасибо," + operation.Destination + ", за покупку!";
            return Json(new JsonResponse(JsonResponseType.Success, equipment.GetEquipmentType() + " успешно продан(-а)!"));
        }

        [HttpPost]
        public ActionResult AddEquipment(AddEquipmentModel equipmentModel)
        {
            int oldPrice = int.Parse(equipmentModel.Price);
            int newPrice = oldPrice + (int)(oldPrice * 0.2);

            var equipment = new Equipment(equipmentModel.Type, equipmentModel.Company, equipmentModel.Model, Status.InStock, newPrice.ToString());
            repo.AddEquipment(equipment);

            var operation = new Operation(Guid.NewGuid(), OperationType.ToStock, oldPrice, equipmentModel.Destination, equipment.Id, DateTime.Now);
            repo.AddOperation(operation);

            return RedirectToAction("InStock", "Shop");
        }

        public ActionResult Operations()
        {
            var operations = repo.GetAllOperation();
            var listOE = new List<OperationEquipment>();
            int cash = 0;

            foreach (var o in operations)
            {
                switch (o.Type)
                {
                    case OperationType.ToStock:
                        cash -= o.Price;
                        break;
                    case OperationType.Sold:
                        cash += o.Price;
                        break;
                    default:
                        break;
                }

                listOE.Add(new OperationEquipment(o, repo.GetEquipmentById(o.EquipmentId)));
            }

            if (listOE.Count > 0)
            {
                ViewBag.DatalistInStock = listOE.Where(o => o.Operation.Type == OperationType.ToStock).ToList();
                ViewBag.DatalistSold = listOE.Where(o => o.Operation.Type == OperationType.Sold).ToList();
            }
            else
            {
                ViewBag.DatalistInStock = new List<OperationEquipment>();
                ViewBag.DatalistSold = new List<OperationEquipment>();
            }

            ViewBag.TotalCash = cash;
            RefreshData();
            return View();
        }

        public ActionResult PurchaseRequisition()
        {
            var result = repo.GetAllPurchaseRequisition();

            var listOE = new List<OperationEquipment>();
            foreach (var o in result)
            {
                listOE.Add(new OperationEquipment(new Operation(o.Id, o.Type, o.Price, o.Destination, o.EquipmentId, o.Time), repo.GetEquipmentById(o.EquipmentId)));
            }

            if (listOE.Count > 0)
            {
                ViewBag.PurchaseRequisition = listOE.Where(o => o.Operation.Type == OperationType.PurchaseRequisition).ToList();
            }
            else
            {
                ViewBag.PurchaseRequisition = new List<OperationEquipment>();
            }

            RefreshData();
            return View();
        }

        [HttpGet]
        public ActionResult SellPurchaseRequisition(string id)
        {
            var operationId = new Guid(id);
            var operation = repo.GetPurchaseRequisitionById(operationId);
            if(operation == null)
            {
                return HttpNotFound();
            }
            var equipment = repo.GetEquipmentById(operation.EquipmentId);
            if (equipment == null)
            {
                return HttpNotFound();
            }

            equipment.Status = Status.Sold;
            repo.ChangeEquipment(equipment);

            repo.DeletePurchaseRequisition(operation.Id);
            operation.Type = OperationType.Sold;
            operation.Time = DateTime.Now;

            repo.AddOperation(new Operation(operation.Id, operation.Type, equipment.Price, operation.Destination, operation.EquipmentId, operation.Time));
            repo.UpdateDatabase();

            ComputerShopHelper.SendMail(operation.Destination, "Вас беспокоит магазин компьютерной техники ComputerShop.\nМы уже подготовили " + equipment.GetEquipmentType() +". Можете прийти по адресу и забрать", true);

            return RedirectToAction("PurchaseRequisition");
        }

        [HttpGet]
        public ActionResult CancelPurchaseRequisition(string id)
        {
            var operationId = new Guid(id);
            var operation = repo.GetPurchaseRequisitionById(operationId);
            if (operation == null)
            {
                return HttpNotFound();
            }

            var equipment = repo.GetEquipmentById(operation.EquipmentId);

            repo.DeletePurchaseRequisition(operation.Id);
            repo.UpdateDatabase();

            ComputerShopHelper.SendMail(operation.Destination, "Вас беспокоит магазин компьютерной техники ComputerShop.\nИзвините, но "+ equipment.GetEquipmentType() + " уже продан(-а).", true);

            return RedirectToAction("PurchaseRequisition");
        }
    }
}