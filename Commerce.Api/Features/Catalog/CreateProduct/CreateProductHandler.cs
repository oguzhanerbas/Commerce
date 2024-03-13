using Commerce.Api.CQRS;
using Commerce.Api.Domain;
using Mapster;
using MediatR;

namespace Commerce.Api.Features.Catalog.CreateProduct;

public record CreateProductResult(int Id);

public record CreateProductCommand(
    string Name,
    string Brand,
    string Category,
    decimal Price,
    string Description,
    string ImageFile) : ICommand<CreateProductResult>;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = command.Adapt<ProductEntity>();
        return new CreateProductResult(0);
    }
}
