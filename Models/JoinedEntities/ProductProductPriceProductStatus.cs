using Warehouse.Models.Enums;

namespace Warehouse.Models.JoinedEntities
{
    public class ProductProductPriceProductStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
    }
}