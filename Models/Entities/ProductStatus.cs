using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Models.Enums;

namespace Warehouse.Models
{
    [Table("ProductStatuses")]
    public class ProductStatus
    {
        /// <summary>
        /// Идентификатор статуса товара.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Статус товара.
        /// </summary>
        public Status Statuses { get; set; }
    }
}