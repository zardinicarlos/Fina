using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core;
using Microsoft.AspNetCore.Mvc;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync).
                WithName("Transactions: GetAll").
                WithSummary("Recupera as transações por período").
                WithDescription("Recupera as transações por período").
                WithOrder(5).
                Produces<Response<List<Transaction>?>>();
        }

        private static async Task<IResult> HandleAsync(ITransactionHandler handler
            , [FromQuery] DateTime? startDate = null
            , [FromQuery] DateTime? endDate = null
            , [FromQuery] int pageNumber = Configuration.DefaultPageNumber
            , [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };
            var response = await handler.GetByPeriodAsync(request);
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
