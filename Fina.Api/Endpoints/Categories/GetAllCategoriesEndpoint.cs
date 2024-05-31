using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync).
                WithName("Categries: GetAll").
                WithSummary("Recupera todas as categorias").
                WithDescription("Recupera todas as categorias").
                WithOrder(5).
                Produces<Response<List<Category>?>>();
        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler
            , [FromQuery] int pageNumber = Configuration.DefaultPageNumber
            , [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoryRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize 
            };
            var response = await handler.GetAllAsync(request);
            if (response.IsSuccess)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
    }
}
