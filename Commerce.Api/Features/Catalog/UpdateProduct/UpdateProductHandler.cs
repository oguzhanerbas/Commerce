using Commerce.Api.CQRS;
using Commerce.Api.Data;
using Commerce.Api.Exceptions;
using Commerce.Api.Features.Catalog.CreateProduct;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Api.Features.Catalog.UpdateProduct;

public record UpdateProductCommand(
    int Id,
    string Name,
    string Brand,
    string Category,
    decimal Price,
    string? Description,
    string? ImageFile) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool Success);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand is required").MinimumLength(3)
            .WithMessage("Minimum length of brand must be greater than 2 letters");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public class UpdateProductHandler(CommerceDbContext db) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await db.Products.FirstOrDefaultAsync(x=>x.Id == command.Id,cancellationToken);
        if (product is null)
        {
            throw new NotFoundException($"Product with Id = {command.Id} is not found");
        }
        // product.Brand = command.Brand;
        // product.Category = command.Category;
        // product.Price = command.Price;

        await db.Products.ExecuteUpdateAsync(setter => setter
            .SetProperty(x => x.Brand, command.Brand)
            .SetProperty(x => x.Category, command.Category)
            , cancellationToken
        );

        return new UpdateProductResult(true);
    }
}