using Carter;
using Commerce.Api.Domain;
using Mapster;
using MediatR;

namespace Commerce.Api.Features.Catalog.GetProducts;

public record GetProductsRequest(int PageNumber = 1, int PageSize = 2);

public record GetProductResponse(IEnumerable<ProductEntity> Products);
// TODO: Entity yerine dto objesi dönülecek. 

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters]GetProductsRequest request, ISender sender) =>
        {
            var query = new GetProductsQuery(request.PageNumber, request.PageSize);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductResponse>();
            return Results.Ok(response);
        });
    }
}