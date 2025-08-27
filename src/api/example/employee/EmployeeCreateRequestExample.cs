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
                FullName = "Vũ Quang Được", 
                Address = "96 Định Công, Hà Nội",
                Note = "Người Hàn",
            };
        }
    }
}
