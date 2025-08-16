using Swashbuckle.AspNetCore.Filters;
using Chefio.Application.Dtos.Employee;

namespace Chefio.Api.Examples.Employee
{
    public class EmployeeCreateRequestExample : IExamplesProvider<EmployeeCreateRequest>
    {
        public EmployeeCreateRequest GetExamples()
        {
            return new EmployeeCreateRequest
            {
                FullName = "John Doe", 
                Address = "123 Main St, Springfield, USA",
                Note = "New employee onboarding",
            };
        }
    }
}
