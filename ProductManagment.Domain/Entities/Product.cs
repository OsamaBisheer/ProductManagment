using ProductManagment.Domain.Entities.Common;

namespace ProductManagment.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}