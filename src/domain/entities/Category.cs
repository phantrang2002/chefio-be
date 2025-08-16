using Chefio.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chefio.Domain.Entities;

public class Category : BaseEntity
{
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; }

}
