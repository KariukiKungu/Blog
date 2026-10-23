namespace LinkDotNet.Blog.Web.Features.Shop;

public interface IShopService
{
    IReadOnlyList<Product> GetProducts();
}
