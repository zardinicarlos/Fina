﻿using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync).
                WithName("Transaction: Create").
                WithSummary("Cria uma nova transação").
                WithDescription("Cria uma nova transação").
                WithOrder(1).
                Produces<Response<Transaction?>>();
        }

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateAsync(request);
            if (response.IsSuccess)
            {
                return TypedResults.Created($"/{response.Data?.Id}", response);
            }
            else
            {
                return TypedResults.BadRequest(response.Data);
            }
        }
    }
}
