using System.Data.Entity;
using Warehouse.Models;
using Warehouse.Models.Entities;

namespace Warehouse.Context
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext() : base("DefaultConnection") => Database.CreateIfNotExists();
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
    }
}