namespace LinkDotNet.Blog.Web.Features.Shop;

public sealed class ShopService : IShopService
{
    private static readonly IReadOnlyList<Product> Products =
    [
        new Product
        {
            Id = 1,
            Name = "Starter Notebook",
            Slug = "starter-notebook",
            Category = "Office",
            Price = 12.99m,
            ImageUrl = "https://images.unsplash.com/photo-1517849845537-4d257902454a?auto=format&fit=crop&w=900&q=80",
            Description = "A compact daily notebook for notes, ideas, and planning."
        },
        new Product
        {
            Id = 2,
            Name = "Coffee Mug",
            Slug = "coffee-mug",
            Category = "Lifestyle",
            Price = 18.50m,
            ImageUrl = "https://images.unsplash.com/photo-1514228742587-6b1558fcca3d?auto=format&fit=crop&w=900&q=80",
            Description = "A premium ceramic mug designed for slow mornings and good coffee."
        },
        new Product
        {
            Id = 3,
            Name = "Travel Bottle",
            Slug = "travel-bottle",
            Category = "Wellness",
            Price = 24.00m,
            ImageUrl = "https://images.unsplash.com/photo-1523362628745-0c100150b504?auto=format&fit=crop&w=900&q=80",
            Description = "Insulated stainless steel bottle that keeps drinks cold for hours."
        },
        new Product
        {
            Id = 4,
            Name = "Desk Lamp",
            Slug = "desk-lamp",
            Category = "Home",
            Price = 39.99m,
            ImageUrl = "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?auto=format&fit=crop&w=900&q=80",
            Description = "A warm LED desk lamp that makes work and reading more comfortable."
        }
    ];

    public IReadOnlyList<Product> GetProducts() => Products;
}
