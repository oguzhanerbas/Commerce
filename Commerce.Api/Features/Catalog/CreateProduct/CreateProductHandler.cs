using Commerce.Api.CQRS;
using Commerce.Api.Data;
using Commerce.Api.Domain;
using FluentValidation;
using Mapster;
using MediatR;

namespace Commerce.Api.Features.Catalog.CreateProduct;

public record CreateProductResult(int Id);

public record CreateProductCommand(
    string Name,
    string Brand,
    string Category,
    decimal Price,
    string? Description,
    string? ImageFile) : ICommand<CreateProductResult>;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand is required").MinimumLength(3)
            .WithMessage("Minimum length of brand must be greater than 2 letters");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    } 
}

public class CreateProductCommandHandler(CommerceDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = command.Adapt<ProductEntity>();
        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
