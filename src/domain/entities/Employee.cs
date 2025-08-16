using Chefio.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chefio.Domain.Entities;

public class Employee : BaseEntity
{
    [Column("full_name")]
    [MaxLength(100)]
    public string? FullName { get; set; }

    [Column("address")]
    [MaxLength(100)]
    public string? Address { get; set; }

    [Column("note")]
    public string? Note { get; set; }
    
    [Column("account_id")]
    public int AccountId { get; set; }

}
