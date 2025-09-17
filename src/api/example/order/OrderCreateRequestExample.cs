using Swashbuckle.AspNetCore.Filters;
using Chefio.Application.Dtos.Order;
using System;

namespace Chefio.Api.Examples.Order
{
    public class OrderCreateRequestExample : IExamplesProvider<OrderCreateRequest>
    {
        public OrderCreateRequest GetExamples()
        {
            return new OrderCreateRequest
            {
                EmployeeId = 1,
                TableId = 5
            };
        }
    }
}
