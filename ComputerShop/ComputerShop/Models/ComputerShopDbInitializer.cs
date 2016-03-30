using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ComputerShop.Infrastructure;

namespace ComputerShop.Models
{
    public class ComputerShopDbInitializer : DropCreateDatabaseAlways<ComputerShopDbContext>
    {
        protected override void Seed(ComputerShopDbContext db)
        {
            db.Equipments.Add(new Equipment(EquipmentType.Computer, "Apple", "MacBook Air", Status.InStock, 1500));
            db.Equipments.Add(new Equipment(EquipmentType.Flash, "Kingston", "300D", Status.Sold, 30));
            db.Equipments.Add(new Equipment(EquipmentType.HardDrive, "Siagate", "5000", Status.InStock, 100));
            db.Equipments.Add(new Equipment(EquipmentType.Computer, "Apple", "MacBook Pro", Status.Sold, 2000));

            base.Seed(db);
        }
    }
}