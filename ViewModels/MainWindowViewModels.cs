using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Input;
using Warehouse.Context;
using Warehouse.Models;
using Warehouse.Models.Entities;
using Warehouse.Models.Enums;
using Warehouse.Models.JoinedEntities;
using Warehouse.ViewModels.Commands;

namespace Warehouse.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModels
    {
        private readonly WarehouseContext _context = new WarehouseContext();

        public ObservableCollection<ProductProductPriceProductStatus> Products { get; set; }
        public ObservableCollection<ProductProductPriceProductStatus> ProductWithStatusAccept { get; set; }
        public ObservableCollection<ProductProductPriceProductStatus> ProductWithStatusToWarehouse { get; set; }
        public ObservableCollection<ProductProductPriceProductStatus> ProductWithStatusSold { get; set; }
        public ProductProductPriceProductStatus SelectedProduct { get; set; }

        public MainWindowViewModels() => InitializeDataBase();

        public ICommand AppProductCommand
        {
            get
            {
                return new CommandBase((obj) =>
                {
                    _context.Products.AddOrUpdate(new Product
                    {
                        Id = SelectedProduct.Id,
                        Name = SelectedProduct.Name,
                        ProductPricesId = _context.ProductPrices.FirstOrDefault(p => p.Price == SelectedProduct.Price).Id,
                        ProductStatusesId = _context.ProductStatuses.FirstOrDefault(p => p.Statuses == SelectedProduct.Status).Id,
                    });
                    _context.SaveChanges();
                    InitializeDataBase();
                });
            }
        }

        private void InitializeDataBase()
        {
            var products = JoinTables(_context.Products, _context.ProductPrices, _context.ProductStatuses);
            Products = products;
            ProductWithStatusAccept = new ObservableCollection<ProductProductPriceProductStatus>(products.Where(p => p.Status == Status.Accept));
            ProductWithStatusToWarehouse = new ObservableCollection<ProductProductPriceProductStatus>(products.Where(p => p.Status == Status.ToWarehouse));
            ProductWithStatusSold = new ObservableCollection<ProductProductPriceProductStatus>(products.Where(p => p.Status == Status.Sold));
        }

        private ObservableCollection<ProductProductPriceProductStatus> JoinTables(IEnumerable<Product> products,
            IEnumerable<ProductPrice> productPrices, IEnumerable<ProductStatus> productStatuses)
        {
            return new ObservableCollection<ProductProductPriceProductStatus>(products
                .Join(productPrices, p => p.ProductPricesId, pp => pp.Id,
                    (p, pp) => new { p.Id, p.Name, p.ProductStatusesId, pp.Price })
                .Join(productStatuses, p => p.ProductStatusesId, ps => ps.Id,
                    (p, ps) => new ProductProductPriceProductStatus { Id = p.Id, Name = p.Name, Price = p.Price, Status = ps.Statuses }));
        }
    }
}