using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync).
                WithName("Categries: Delete").
                WithSummary("Remove uma categoria").
                WithDescription("Remove uma categoria").
                WithOrder(3).
                Produces<Response<Category?>>();
        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id) //Este id é o id recebido na rota app.MapDelete
        {
            var request = new DeleteCategoryRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };
            var response = await handler.DeleteAsync(request);
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
