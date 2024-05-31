using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class CategoryHandler : ICategoryHandler
    {
        private AppDbContext appDbContext;

        public CategoryHandler(AppDbContext context) 
        { 
            appDbContext = context;
        }

        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category()
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
            };
            try
            {
                await appDbContext.Categories.AddAsync(category);
                await appDbContext.SaveChangesAsync();
                return new Response<Category?>(category, 201, "Categoria criada com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Category?>(null, 500, "Não foi possível criar a categoria");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category == null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                appDbContext.Categories.Remove(category);
                await appDbContext.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Categoria excluida com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Category?>(null, 500, "Não foi possível criar a categoria");
            }
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryRequest request)
        {
            try
            {
                var query = appDbContext.Categories.AsNoTracking().Where(x => x.UserId == request.UserId).OrderBy(x => x.Title);

                var categories = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(categories, count, request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<Category>?>(null, 500, "Não foi recuperar as categorias");
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await appDbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category == null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                return new Response<Category?>(category);
            }
            catch (Exception ex)
            {
                return new Response<Category?>(null, 500, "Não foi recuperar criar a categoria");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category == null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                category.Title = request.Title;
                category.Description = request.Description;

                appDbContext.Categories.Update(category);
                await appDbContext.SaveChangesAsync();
                
                return new Response<Category?>(category, 200, "Categoria atualizada com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Category?>(null, 500, "Não foi possível criar a categoria");
            }
        }
    }
}
