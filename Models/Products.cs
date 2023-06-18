using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Cors;
using Microsoft.Build.Framework;

namespace OnlineShop.Models
{
	public class Products
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
		public string Image { get; set; }
		[DisplayName("Product Color")]
		public string ProductColor { get; set; }
        [Required]
		[DisplayName("Available")]
        public bool	IsAvailable { get; set; }

		[DisplayName("Product Type")]
		[Required]
		public int ProductTypeId { get; set; }

		[ForeignKey("ProductTypeId")]
		public ProductTypes ProductTypes { get; set; }

		[DisplayName("Special Tag")]
		[Required]	
        public int SpecialTagId { get; set; }
        [ForeignKey("SpecialTagId")]
        public SpecialTag SpecialTag { get; set; }

        
	}
}
