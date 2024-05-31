﻿using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync).
                WithName("Categries: GetById").
                WithSummary("Recupera uma categoria").
                WithDescription("Recupera uma categoria").
                WithOrder(4).
                Produces<Response<Category?>>();
        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id) //Este id é o id recebido na rota app.MapGet
        {
            var request = new GetCategoryByIdRequest
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
