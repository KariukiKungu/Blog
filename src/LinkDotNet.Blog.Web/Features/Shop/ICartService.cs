namespace LinkDotNet.Blog.Web.Features.Shop;

public interface ICartService
{
    Task<IReadOnlyList<CartItem>> GetCartAsync();
    Task AddItemAsync(string productId, int quantity = 1);
    Task UpdateQuantityAsync(string productId, int quantity);
    Task RemoveItemAsync(string productId);
    Task ClearAsync();
    Task<int> GetItemCountAsync();
    Task<decimal> GetTotalAsync();
}
