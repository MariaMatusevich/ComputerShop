using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerShop.Models
{
    public class OperationEquipment
    {
        public Operation Operation { get; set; }
        public Equipment Equipment { get; set; }

        public OperationEquipment(Operation operation, Equipment equipment)
        {
            Operation = operation;
            Equipment = equipment;
        }
    }
}