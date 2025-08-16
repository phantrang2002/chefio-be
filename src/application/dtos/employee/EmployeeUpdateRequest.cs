namespace Chefio.Application.Dtos.Employee;

public class EmployeeUpdateRequest
{
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Note { get; set; }
    public int? AccountId { get; set; }
}