﻿namespace Commerce.Api.Domain;

public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public decimal Price { get; set; }

}