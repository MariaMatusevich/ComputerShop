using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class Equipment
    {
        [Key]
        public Guid Id { get; set; }
        public EquipmentType Type { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public Status Status { get; set; }
        public int Price { get; set; }
        

        public Equipment(EquipmentType type, string company, string model, Status status, int price)
        {
            Id = Guid.NewGuid();
            Type = type;
            Company = company;
            Model = model;
            Status = status;
            Price = price;
        }

        public Equipment(Guid id, EquipmentType type, string company, string model, Status status, int price)
        {
            Id = id;
            Type = type;
            Company = company;
            Model = model;
            Status = status;
            Price = price;
        }

        public Equipment(string type, string company, string model, Status status, string price)
        {
            Id = Guid.NewGuid();
            Type = SetTypeFromString(type);
            Company = company;
            Model = model;
            Status = status;
            Price = int.Parse(price);
        }

        public Equipment() : this(EquipmentType.Computer, "UNKNOWN", "UNKNOWN", Status.Sold, -111)
        {

        }

        public EquipmentType SetTypeFromString(string type)
        {
            switch (type)
            {
                case "Компьютер":
                    return EquipmentType.Computer;
                case "Флеш память":
                    return EquipmentType.Flash;
                case "Жесткий диск":
                    return EquipmentType.HardDrive;
                default:
                    return EquipmentType.UNKNOWN;
            }
        }

        public string GetEquipmentType()
        {
            switch (Type)
            {
                case EquipmentType.Computer:
                    return "Компьютер";
                case EquipmentType.Flash:
                    return "Флеш память";
                case EquipmentType.HardDrive:
                    return "Жесткий диск";
                case EquipmentType.UNKNOWN:
                    return "Unknown Type";
            }
            return "Unknown Type";
        }

        public string GetStatus()
        {
            switch(Status)
            {
                case Status.InStock:
                    return "Есть в наличии";
                case Status.Sold:
                    return "Продано";
            }

            return "Unknown Status";
        }
    }

    public class AddEquipmentModel
    {
        public string Company { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public string Type { get; set; }

        public string Destination { get; set; }


        public AddEquipmentModel(string company, string model, string type, string price, string destination)
        {
            Company = company;
            Type = type;
            Model = model;
            Price = price;
            Destination = destination;
        }

        public AddEquipmentModel() : this("UNKNOWN", "UNKNOWN", "UNKNOWN", "UNKNOWN", "UNKNOWN")
        {

        }
    }
}