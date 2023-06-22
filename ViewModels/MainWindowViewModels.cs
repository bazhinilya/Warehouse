using PropertyChanged;
using System;
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
        public ObservableCollection<ProductProductPriceProductStatus> Products { get; set; }
        public ObservableCollection<ProductProductPriceProductStatus> ProductWithStatusAccept { get; set; }
        public ObservableCollection<ProductProductPriceProductStatus> ProductWithStatusToWarehouse { get; set; }
        public ObservableCollection<ProductProductPriceProductStatus> ProductWithStatusSold { get; set; }
        public ProductProductPriceProductStatus SelectedProduct { get; set; }

        public MainWindowViewModels() => InitializeDataBase();

        public ICommand AddOrUpdateProductCommand
        {
            get
            {
                return new CommandBase((obj) =>
                {
                    if (SelectedProduct != null)
                    {
                        using (var context = new WarehouseContext())
                        {
                            var productFromDb = context.Products.FirstOrDefault(p => p.Id == SelectedProduct.Id);
                            context.Products.AddOrUpdate(new Product
                            {
                                Id = SelectedProduct.Id,
                                Name = SelectedProduct.Name,
                                ProductPricesId = context.ProductPrices.FirstOrDefault(p => p.Price == SelectedProduct.Price).Id,
                                ProductStatusesId = context.ProductStatuses.FirstOrDefault(p => p.Statuses == SelectedProduct.Status).Id,
                                DateAddProduct = SelectedProduct.DateAddProduct == null ? DateTime.Now : productFromDb.DateAddProduct,
                                DateAccept = productFromDb?.DateAccept == null && SelectedProduct.Status == Status.Accept ? DateTime.Now : productFromDb?.DateAccept,
                                DateToWarehouse = productFromDb?.DateToWarehouse == null && SelectedProduct.Status == Status.ToWarehouse ? DateTime.Now : productFromDb?.DateToWarehouse,
                                DateSold = productFromDb?.DateSold == null && SelectedProduct.Status == Status.Sold ? DateTime.Now : productFromDb?.DateSold
                            });
                            context.SaveChanges();
                        }
                        InitializeDataBase();
                    }
                });
            }
        }

        private void InitializeDataBase()
        {
            using (var context = new WarehouseContext())
            {
                var products = JoinTables(context.Products, context.ProductPrices, context.ProductStatuses);
                Products = products;
                ProductWithStatusAccept = new ObservableCollection<ProductProductPriceProductStatus>(products.Where(p => p.Status == Status.Accept));
                ProductWithStatusToWarehouse = new ObservableCollection<ProductProductPriceProductStatus>(products.Where(p => p.Status == Status.ToWarehouse));
                ProductWithStatusSold = new ObservableCollection<ProductProductPriceProductStatus>(products.Where(p => p.Status == Status.Sold));
            }
        }

        private ObservableCollection<ProductProductPriceProductStatus> JoinTables(IEnumerable<Product> products,
            IEnumerable<ProductPrice> productPrices, IEnumerable<ProductStatus> productStatuses)
        {
            return new ObservableCollection<ProductProductPriceProductStatus>(products
                .Join(productPrices, p => p.ProductPricesId, pp => pp.Id,
                    (p, pp) => new
                    {
                        p.Id,
                        p.Name,
                        p.ProductStatusesId,
                        pp.Price,
                        p.DateAddProduct,
                        p.DateAccept,
                        p.DateToWarehouse,
                        p.DateSold
                    })
                .Join(productStatuses, p => p.ProductStatusesId, ps => ps.Id,
                    (p, ps) => new ProductProductPriceProductStatus
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Status = ps.Statuses,
                        DateAddProduct = p.DateAddProduct,
                        DateAccept = p.DateAccept,
                        DateToWarehouse = p.DateToWarehouse,
                        DateSold = p.DateSold
                    }));
        }
    }
}