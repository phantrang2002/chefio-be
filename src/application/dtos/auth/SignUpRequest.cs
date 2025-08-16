using System.ComponentModel.DataAnnotations;
using Chefio.Domain.Enums;

public class SignUpRequest
{
    public string Username { get; set; }
    public string Password { get; set; }

    [Range((int)Role.Admin, (int)Role.Accountant, ErrorMessage = "Invalid role")]
    public Role Role { get; set; }
}