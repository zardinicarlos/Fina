using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync).
                WithName("Transaction: GetById").
                WithSummary("Recupera uma transação").
                WithDescription("Recupera uma transação").
                WithOrder(4).
                Produces<Response<Transaction?>>();
        }

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id) //Este id é o id recebido na rota app.MapGet
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };
            var response = await handler.GetByIdAsync(request);
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
