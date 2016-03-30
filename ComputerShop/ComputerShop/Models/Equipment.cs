using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        public Equipment() : this(EquipmentType.Computer, "UNKNOWN", "UNKNOWN", Status.Sold, -111)
        {

        }

        public string GetType()
        {
            switch (Type)
            {
                case EquipmentType.Computer:
                    return "Компьютер";
                case EquipmentType.Flash:
                    return "Флеш память";
                case EquipmentType.HardDrive:
                    return "Жесткий диск";
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

    public enum EquipmentType
    {
        Computer,
        Notebook,
        Mouse,
        Monitor,
        Flash,
        HardDrive
    }

    public enum Status
    {
        InStock,
        Sold
    }
}