using Swashbuckle.AspNetCore.Filters;
using Chefio.Application.Dtos.Category;

namespace Chefio.Api.Examples.Category
{
    public class CategoryCreateRequestExample : IExamplesProvider<CategoryCreateRequest>
    {
        public CategoryCreateRequest GetExamples()
        {
            return new CategoryCreateRequest
            {
                Name = "Món chính"
            };
        }
    }
}
