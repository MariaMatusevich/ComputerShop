using ComputerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerShop.Infrastructure
{

    public class ComputerShopRepository
    {
        private ComputerShopDbContext _db;

        public ComputerShopRepository()
        {
            _db = new ComputerShopDbContext();
        }

        public void UpdateDatabase()
        {
            _db.SaveChanges();
        }

        #region [ Equipment ]
        public Equipment GetEquipmentById(Guid id)
        {
            return _db.Equipments.Where(o => o.Id == id).FirstOrDefault();
        }

        public void AddEquipment(Equipment equipment)
        {
            _db.Equipments.Add(equipment);
            UpdateDatabase();
        }

        public void DeleteEquipment(Guid id)
        {
            var equipment = GetEquipmentById(id);
            if (equipment == null)
            {
                return;
            }
            _db.Equipments.Remove(equipment);
            UpdateDatabase();
        }

        public List<Equipment> GetAllEquipment()
        {
            return _db.Equipments.ToList();
        }

        public void ChangeEquipment(Equipment equipment)
        {
            DeleteEquipment(equipment.Id);
            AddEquipment(equipment);
        }

        public List<Equipment> GetEquipmentsByStatus(Status status)
        {
            return _db.Equipments.Where(o => o.Status == status).ToList();
        }

        #endregion

        #region [ Operation ]

        public Operation GetOperationById(Guid id)
        {
            return _db.Operations.Where(o => o.Id == id).FirstOrDefault();
        }

        public Operation GetOperationByEquipmentId(Guid id)
        {
            return _db.Operations.Where(o => o.EquipmentId == id).FirstOrDefault();
        }

        public void AddOperation(Operation operation)
        {
            _db.Operations.Add(operation);
            UpdateDatabase();
        }

        public void DeleteOperation(Guid id)
        {
            var operation = GetOperationById(id);
            if (operation == null)
            {
                return;
            }
            _db.Operations.Remove(operation);
            UpdateDatabase();
        }

        public List<Operation> GetAllOperation()
        {
            return _db.Operations.ToList();
        }

        public void ChangeOperation(Operation operation)
        {
            DeleteOperation(operation.Id);
            AddOperation(operation);
            UpdateDatabase();
        }

        #endregion

        #region [ PurchaseRequisition ]

        public List<PurchaseRequisition> GetAllPurchaseRequisition()
        {
            return _db.PurchaseRequisitions.ToList();
        }

        public PurchaseRequisition GetPurchaseRequisitionById(Guid id)
        {
            return _db.PurchaseRequisitions.Where(o => o.Id == id).FirstOrDefault();
        }

        public void AddPurchaseRequisition(Operation operation)
        {
            _db.PurchaseRequisitions.Add(new PurchaseRequisition(operation));
            UpdateDatabase();
        }

        public void DeletePurchaseRequisition(Guid id)
        {
            var o = GetPurchaseRequisitionById(id);
            if (o != null)
            {
                _db.PurchaseRequisitions.Remove(o);
                UpdateDatabase();
            }
        }

        #endregion
    }
}