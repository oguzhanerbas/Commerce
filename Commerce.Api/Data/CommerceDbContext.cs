using Commerce.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Api.Data;

public class CommerceDbContext(DbContextOptions<CommerceDbContext> options) : DbContext(options)
{
    public virtual DbSet<ProductEntity> Products { get; set; }
}