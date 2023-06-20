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
    }
}