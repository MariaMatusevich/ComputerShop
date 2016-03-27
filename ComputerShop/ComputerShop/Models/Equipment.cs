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