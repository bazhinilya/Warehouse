using System;
using Warehouse.Models.Enums;

namespace Warehouse.Models.JoinedEntities
{
    public class ProductProductPriceProductStatus
    {
        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название товара.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена товара.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Статус товара.
        /// </summary>
        public Status Status { get; set; }
        
        /// <summary>
        /// Дата добавления продукта.
        /// </summary>
        public DateTime? DateAddProduct { get; set; }

        /// <summary>
        /// Дата перехода продукта в статус "Принят".
        /// </summary>
        public DateTime? DateAccept { get; set; }

        /// <summary>
        /// Дата перехода продукта в статус "На складе".
        /// </summary>
        public DateTime? DateToWarehouse { get; set; }

        /// <summary>
        /// Дата перехода продукта в статус "Продан".
        /// </summary>
        public DateTime? DateSold { get; set; }
    }
}