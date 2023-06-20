namespace Warehouse.Models.Entities
{
    public class ProductPrice
    {
        /// <summary>
        /// Идентификатор цены товара.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Цена товара.
        /// </summary>
        public decimal Price { get; set; }
    }
}