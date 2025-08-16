using Chefio.Domain.Common;
using Chefio.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chefio.Domain.Entities;

public class Account : BaseEntity
{
    [Column("username")]
    public string Username { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("role")]
    public Role Role { get; set; }

}
