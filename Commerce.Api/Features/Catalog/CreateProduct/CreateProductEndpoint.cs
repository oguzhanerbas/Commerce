using Carter;
using Mapster;
using MediatR;

namespace Commerce.Api.Features.Catalog.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Brand,
    string Category,
    decimal Price,
    string Description,
    string ImageFile);

public record CreateProductResponse(int Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        });
    }
}