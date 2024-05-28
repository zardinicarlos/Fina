using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fina.Core.Responses;

namespace Fina.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
        Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request);
    }
}
