using System.ComponentModel.DataAnnotations;
using ProductManagment.Domain.ViewModels.Common;

namespace ProductManagment.Domain.ViewModels.Product
{
    public class ProductUpdateVM : AuditableVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public float Price { get; set; }
    }
}