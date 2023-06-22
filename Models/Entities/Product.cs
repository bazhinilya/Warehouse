using System;

namespace Warehouse.Models
{
    public class Product
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
        /// Идентификатор, связывющий с таблицей ProductStatuses.
        /// </summary>
        public int ProductStatusesId { get; set; }

        /// <summary>
        /// Идентификатор, связывющий с таблицей ProductPrices.
        /// </summary>
        public int ProductPricesId { get; set; }

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