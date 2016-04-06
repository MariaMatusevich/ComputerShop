using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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


        public Operation(Guid operationId, OperationType type, string destination, Guid equipmentId, DateTime time)
        {
            Id = Guid.NewGuid();
            Type = type;
            Destination = destination;
            EquipmentId = equipmentId;
            Time = time;
        }

        public string GetOperationTypeString()
        {
            switch(Type)
            {
                case OperationType.Sold:
                    return "Продано";
                case OperationType.ToStock:
                    return "Закуплено";
                default:
                    return "UNKNOWN";
            }
        }

        public Operation() : this(Guid.NewGuid(), OperationType.Sold, "UNKNOWN", Guid.NewGuid(), DateTime.MinValue)
        {

        }

}
    public enum OperationType
    {
        ToStock,
        Sold
    }
}