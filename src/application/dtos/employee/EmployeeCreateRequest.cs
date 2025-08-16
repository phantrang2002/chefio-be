using System.ComponentModel.DataAnnotations;

namespace Chefio.Application.Dtos.Employee;

public class EmployeeCreateRequest
{
    [Required(ErrorMessage = "Full name is required.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Note is required.")]
    public string Note { get; set; }

    [Required(ErrorMessage = "Account Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Account Id is required.")]
    public int AccountId { get; set; }
}