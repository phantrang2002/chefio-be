using Chefio.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chefio.Domain.Entities;

public class Dish : BaseEntity
{
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Column("description")]
    public string? description { get; set; }

    [Column("photo")]
    public string? photo { get; set; }

    [Column("price", TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column("is_available")]
    public bool isAvailable { get; set; }

} 