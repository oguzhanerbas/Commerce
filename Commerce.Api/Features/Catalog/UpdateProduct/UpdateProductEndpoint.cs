using Carter;
using Commerce.Api.Features.Catalog.CreateProduct;
using Mapster;
using MediatR;

namespace Commerce.Api.Features.Catalog.UpdateProduct;

public record UpdateProductRequest(
    int Id,
    string Name,
    string Brand,
    string Category,
    decimal Price,
    string Description,
    string ImageFile);

public record UpdateProductResponse(bool Success);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        }).WithName("UpdateProduct")
        .Produces<UpdateProductResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}