using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace OnlineShop.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [DisplayName("Product Color")]
        public string ProductColor { get; set; }
        public bool IsAvailable { get; set; }
        [DisplayName("Product Type")]
        public int ProductTypeId { get; set; }
        [DisplayName("Special Tag")]
        public int SpecialTagId { get; set; }
        
        public IFormFile Image { get; set; }
    }
}
