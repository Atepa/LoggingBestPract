using System.ComponentModel.DataAnnotations.Schema;

namespace LoggingBestPract.Domain.Common
{
    public abstract class EntityBase<TKey>    
    {
        [Column("id")]
        public int Id { get; set; }
        
     
    }
}
