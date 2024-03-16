using Carter;
using Mapster;
using MediatR;

namespace Commerce.Api.Features.Catalog.DeleteProduct;

public record DeleteProductResponse(bool Success);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (int id, ISender sender) =>
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteProductResponse>();
            return Results.Ok(response);
        });
    }
}