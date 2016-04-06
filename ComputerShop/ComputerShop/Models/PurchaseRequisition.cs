using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComputerShop.Models
{
    public class PurchaseRequisition
    {
        [Key]
        public Guid Id { get; set; }
        public OperationType Type { get; set; }
        public string Destination { get; set; }
        public Guid EquipmentId { get; set; }
        public DateTime Time { get; set; }

        public PurchaseRequisition(Operation operation)
        {
            Id = operation.Id;
            Type = operation.Type;
            Destination = operation.Destination;
            EquipmentId = operation.EquipmentId;
            Time = operation.Time;
        }

        public PurchaseRequisition() : this(new Operation())
        {
        }
    }
}