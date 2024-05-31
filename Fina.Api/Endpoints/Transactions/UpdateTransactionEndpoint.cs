using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandleAsync).
                WithName("Transactions: Update").
                WithSummary("Atualiza uma transação").
                WithDescription("Atualiza uma transação").
                WithOrder(2).
                Produces<Response<Category?>>();
        }

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id) //Este id é o id recebido na rota app.MapGet
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var response = await handler.UpdateAsync(request);
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
