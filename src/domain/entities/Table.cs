using Chefio.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chefio.Domain.Entities;

public class Table : BaseEntity
{ 
    [Column("name")]
    [MaxLength(32)]
    public string Name { get; set; }

    [Column("is_available")]
    public bool isAvailable { get; set; }
} 