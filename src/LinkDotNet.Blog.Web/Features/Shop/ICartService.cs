namespace LinkDotNet.Blog.Web.Features.Shop;

public interface ICartService
{
    Task<IReadOnlyList<CartItem>> GetCartAsync();
    Task AddItemAsync(int productId, int quantity = 1);
    Task UpdateQuantityAsync(int productId, int quantity);
    Task RemoveItemAsync(int productId);
    Task ClearAsync();
    Task<int> GetItemCountAsync();
    Task<decimal> GetTotalAsync();
}
