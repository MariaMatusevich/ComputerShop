using System;
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
        public JsonResult Buy(Operation operation)
        {
            var equipment = repo.GetEquipmentById(operation.EquipmentId);
            if (equipment == null)
            {
                return Json(new JsonResponse(JsonResponseType.Error, "Такого товара в базе нету. Обновите, пожалуйста, страницу."));
            }

            operation.Time = DateTime.Now;
            operation.Type = OperationType.PurchaseRequisition;
            // добавляем информацию о покупке в базу данных
            repo.AddPurchaseRequisition(operation);
            // сохраняем в бд все изменения
            repo.UpdateDatabase();
            //return "Спасибо," + operation.Destination + ", за покупку!";
            return Json(new JsonResponse(JsonResponseType.Success, "Вы успешно подали заявку на покупку: " +equipment.GetEquipmentType() + " х 1шт. Наши специалисты скоро с Вами свящутся."));
        }
    }
}