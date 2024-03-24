using Commerce.Api.CQRS;
using Commerce.Api.Data;
using Commerce.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Api.Features.Catalog.GetProducts;

public class ProductModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public decimal Price { get; set; }
}

public record GetProductsResult(IEnumerable<ProductEntity> Products);

public record GetProductsQuery(int PageNumber = 1, int PageSize = 2) : IQuery<GetProductsResult>;

public class GetProductsHandler(CommerceDbContext db) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await db.Products.Skip((query.PageNumber-1) * query.PageSize).Take(query.PageSize).ToListAsync(cancellationToken);
        //var products = await db.Products.Skip(query.PageNumber * query.PageSize).Take(query.PageSize).Select(x=> new ProductModel(){Brand = x.Brand, Category = x.Category}).ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}