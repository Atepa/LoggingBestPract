using System.ComponentModel.DataAnnotations.Schema;

namespace LoggingBestPract.Domain.Entities;

public abstract class Entity<TKey>
{
    [Column("id")]
    public TKey Id { get; set; }
    
    [Column("CreatedDate")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}