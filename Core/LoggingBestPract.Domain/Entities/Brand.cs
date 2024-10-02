using System.ComponentModel.DataAnnotations.Schema;
using LoggingBestPract.Domain.Common;

namespace LoggingBestPract.Domain.Entities
{    
    [Table("Brands")]
    public class Brands : Entity<int>
    {
        public Brands(string name, string serviceAdress)
        {
            Name = name;
            ServiceAdress = serviceAdress;
        }
        
        [Column("Name")]
        public string Name { get; set; }
        
        [Column("ServiceAdress")]
        public string ServiceAdress { get; set; }
    }
}
