using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync).
                WithName("Transaction: Delete").
                WithSummary("Remove uma transação").
                WithDescription("Remove uma transação").
                WithOrder(3).
                Produces<Response<Transaction?>>();
        }

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id) //Este id é o id recebido na rota app.MapDelete
        {
            var request = new DeleteTransactionRequest
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
