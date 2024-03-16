using Commerce.Api.CQRS;
using Commerce.Api.Data;
using Commerce.Api.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Api.Features.Catalog.DeleteProduct;

public record DeleteProductResult(bool Success);

public record DeleteProductCommand(int Id) : ICommand<DeleteProductResult>;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required").GreaterThan(0)
            .WithMessage("Id must be greater than 0");
    }
}

public class DeleteProductHandler(CommerceDbContext db) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await db.Products.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
        if (product == null)
        {
            throw new NotFoundException($"Product with Id = {command.Id} is not found");
        }

        db.Products.Remove(product);
        await db.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}