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
            var list = new List<Equipment>();
            list.Add(new Equipment(new Guid("C0D01975-2981-4920-98CD-9210F7B504BC"), EquipmentType.Computer, "Apple", "MacBook Air", Status.InStock, 20200000));
            list.Add(new Equipment(new Guid("990B36D5-8699-418B-9253-2A549B898801"), EquipmentType.Computer, "Dell", "Alienware Area-51 R2", Status.InStock, 119323000));
            list.Add(new Equipment(new Guid("E6BEEC4F-404E-4981-86BC-F8B1230521E0"), EquipmentType.Computer, "Rednox", "Redhost DL10346", Status.InStock, 4035100));
            list.Add(new Equipment(new Guid("D90614F6-8C50-47E7-9370-8B9668BDF1BA"), EquipmentType.Computer, "Evolution", "PRO GAMER 17484", Status.InStock, 30550000));
            list.Add(new Equipment(new Guid("8AF84A69-C335-4B5F-8C81-579AC06C396F"), EquipmentType.Computer, "HP", "ProDesk 400 G2 ", Status.InStock, 5802400));
            list.Add(new Equipment(new Guid("509FB061-CA25-4058-9E3B-B8F95036CB9D"), EquipmentType.HardDrive, "WD", "Elements Portable", Status.InStock, 1195800));
            list.Add(new Equipment(new Guid("B0F4A593-2BC9-4BA9-B25C-FFD949B0C188"), EquipmentType.HardDrive, "Seagate", "Backup Plus Portable", Status.InStock, 1244300));
            list.Add(new Equipment(new Guid("CD03F074-DD9E-4402-84DA-121950EE9513"), EquipmentType.HardDrive, "Transcend", "StoreJet 25H3P", Status.InStock, 1230100));
            list.Add(new Equipment(new Guid("71E45506-A783-4E7F-9A48-4031CA343EB5"), EquipmentType.HardDrive, "Silicon-Power", "Armor A60", Status.InStock, 1303800));
            list.Add(new Equipment(new Guid("A64FC97A-2C7E-4486-BA9A-CA267FD1E920"), EquipmentType.HardDrive, "Toshiba", "Canvio Basics", Status.InStock, 1161300));
            list.Add(new Equipment(new Guid("820D2D88-765C-4EFC-889F-F7220E742DF3"), EquipmentType.Flash, "Transcend", "JetFlash 700", Status.InStock, 198300));
            list.Add(new Equipment(new Guid("8F872B0C-FB49-4883-B7CD-B5A05A3F9F3A"), EquipmentType.Flash, "Kingston", "DataTraveler SE9 G2", Status.InStock, 225200));
            list.Add(new Equipment(new Guid("2E0704B4-EAC9-49B8-8555-348AB18FE905"), EquipmentType.Flash, "Silicon-Power", "Jewel J10", Status.InStock, 266000));
            list.Add(new Equipment(new Guid("0D3D54D0-4893-4765-87A9-764C30BC177A"), EquipmentType.Flash, "SanDisk", "Cruzer Fit", Status.InStock, 64100));
            list.Add(new Equipment(new Guid("BDDBC2B1-D0BD-4391-A23A-52083EF89A38"), EquipmentType.Flash, "Samsung", "MUF-32CB", Status.InStock, 198300));
            list.Add(new Equipment(new Guid("57736EB2-0F99-4CE8-A33C-82D59F759485"), EquipmentType.Flash, "Kingston", "300D", Status.Sold, 205000));
            list.Add(new Equipment(new Guid("2FE81688-BD37-41EC-982F-C9EDA80E6A59"), EquipmentType.Computer, "Apple", "MacBook Pro", Status.Sold, 25790000));


            foreach (var l in list)
            {
                int oldPrice = l.Price;
                int newPrice = oldPrice + (int)(oldPrice * 0.2);
                l.Price = newPrice;

                db.Equipments.Add(l);

                if (l.Status == Status.InStock)
                {
                    var operation = new Operation(Guid.NewGuid(), OperationType.ToStock, oldPrice, "MIPS", l.Id, DateTime.Now);
                    db.Operations.Add(operation);
                }
                if (l.Status == Status.Sold)
                {
                    var operation = new Operation(Guid.NewGuid(), OperationType.Sold, oldPrice, "", l.Id, DateTime.Now);
                    db.Operations.Add(operation);
                }
            }



            /*int oldPrice = int.Parse(equipmentModel.Price);
            int newPrice = oldPrice + (int)(oldPrice * 0.2);

            var equipment = new Equipment(equipmentModel.Type, equipmentModel.Company, equipmentModel.Model, Status.InStock, newPrice.ToString());
            repo.AddEquipment(equipment);

            var operation = new Operation(Guid.NewGuid(), OperationType.ToStock, oldPrice, equipmentModel.Destination, equipment.Id, DateTime.Now);*/
            base.Seed(db);
        }
    }
}