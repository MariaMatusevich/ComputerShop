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

        #region [ Equipment ]
        public Equipment GetEquipmentById(Guid id)
        {
            return _db.Equipments.Where(o => o.Id == id).FirstOrDefault();
        }

        public void AddEquipment(Equipment equipment)
        {
            _db.Equipments.Add(equipment);

        }

        public void DeleteEquipment(Guid id)
        {
            var equipment = GetEquipmentById(id);
            if (equipment == null)
            {
                return;
            }
            _db.Equipments.Remove(equipment);
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

        #endregion

        #region [ Operation ]

        public Operation GetOperationById(Guid id)
        {
            return _db.Operations.Where(o => o.Id == id).FirstOrDefault();
        }

        public void AddOperation(Operation operation)
        {
            _db.Operations.Add(operation);

        }

        public void DeleteOperation(Guid id)
        {
            var operation = GetOperationById(id);
            if (operation == null)
            {
                return;
            }
            _db.Operations.Remove(operation);
        }

        public List<Operation> GetAllOperation()
        {
            return _db.Operations.ToList();
        }

        public void ChangeOperation(Operation operation)
        {
            DeleteOperation(operation.Id);
            AddOperation(operation);
        }

        #endregion
    }
}