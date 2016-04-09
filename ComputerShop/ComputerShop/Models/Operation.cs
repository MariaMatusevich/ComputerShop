using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class Operation
    {
        [Key]
        public Guid Id { get; set; }
        public OperationType Type { get; set; }
        public string Destination { get; set; }
        public Guid EquipmentId { get; set; }
        public DateTime Time { get; set; }
        public int Price { get; set; }


        public Operation(Guid operationId, OperationType type, int price, string destination, Guid equipmentId, DateTime time)
        {
            if (operationId != null)
            {
                Id = operationId;
            }
            else
            {
                Id = Guid.NewGuid();
            }
            Type = type;
            Price = price;
            Destination = destination;
            EquipmentId = equipmentId;
            Time = time;

        }

        public string GetOperationTypeString()
        {
            switch (Type)
            {
                case OperationType.Sold:
                    return "Продано";
                case OperationType.ToStock:
                    return "Закуплено";
                case OperationType.PurchaseRequisition:
                    return "Заявка на покупку";
                default:
                    return "UNKNOWN";
            }
        }

        public Operation() : this(Guid.NewGuid(), OperationType.Sold, -111, "UNKNOWN", Guid.NewGuid(), DateTime.MinValue)
        {

        }

    }

}