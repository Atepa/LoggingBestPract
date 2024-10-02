using System.ComponentModel.DataAnnotations.Schema;
using LoggingBestPract.Domain.Common;

namespace LoggingBestPract.Domain.Entities
{    
    [Table("Products")]
    public class Products : Entity<int>
    {
        public Products(string title, string description, int brandId, decimal price, decimal discount, string brandName)
        {
            Title = title;
            Description = description;
            BrandId = brandId;
            Price = price;
            Discount = discount;
            BrandName = brandName;
        }
        
        [Column("Title")]
        public string Title { get; set; }
        
        [Column("Description")]
        public string Description { get; set; }
        
        [Column("BrandId")]
        public int BrandId { get; set; }
        
        [Column("Price")]
        public decimal Price { get; set; }
        
        [Column("Discount")]
        public decimal Discount { get; set; }
        
        [Column("BrandName")]
        public string BrandName { get; set; }
    }
}
