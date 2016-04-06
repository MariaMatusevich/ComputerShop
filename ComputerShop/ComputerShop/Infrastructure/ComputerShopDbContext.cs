using ComputerShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComputerShop.Infrastructure
{
    public class ComputerShopDbContext : DbContext
    {
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<PurchaseRequisition> PurchaseRequisitions { get; set; }
    }
}