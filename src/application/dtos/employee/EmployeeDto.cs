namespace Chefio.Application.Dtos.Employee;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Note { get; set; }
    public int AccountId { get; set; }
    public AccountDto Account { get; set; }

}

public class AccountDto
{
    public int Id { get; set; }
    public string Role { get; set; }
}