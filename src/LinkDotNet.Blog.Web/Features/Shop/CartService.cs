using LinkDotNet.Blog.Web.Features.Services;

namespace LinkDotNet.Blog.Web.Features.Shop;

public sealed class CartService : ICartService
{
    private const string CartStorageKey = "shop-cart";
    private readonly ILocalStorageService localStorageService;
    private readonly IShopService shopService;

    public CartService(ILocalStorageService localStorageService, IShopService shopService)
    {
        this.localStorageService = localStorageService;
        this.shopService = shopService;
    }

    public async Task<IReadOnlyList<CartItem>> GetCartAsync()
    {
        await EnsureCartExistsAsync();
        return await localStorageService.GetItemAsync<List<CartItem>>(CartStorageKey);
    }

    public async Task AddItemAsync(string productId, int quantity = 1)
    {
        if (string.IsNullOrWhiteSpace(productId) || quantity <= 0)
        {
            return;
        }

        await EnsureCartExistsAsync();
        var cart = await localStorageService.GetItemAsync<List<CartItem>>(CartStorageKey);
        var product = shopService.GetProducts().SingleOrDefault(p => p.Id == productId);

        if (product is null)
        {
            throw new InvalidOperationException($"Product {productId} was not found.");
        }

        var existingItem = cart.FirstOrDefault(item => item.ProductId == productId);

        if (existingItem is null)
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Quantity = quantity,
            });
        }
        else
        {
            existingItem.Quantity += quantity;
        }

        await localStorageService.SetItemAsync(CartStorageKey, cart);
    }

    public async Task UpdateQuantityAsync(string productId, int quantity)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            return;
        }

        if (quantity <= 0)
        {
            await RemoveItemAsync(productId);
            return;
        }

        await EnsureCartExistsAsync();
        var cart = await localStorageService.GetItemAsync<List<CartItem>>(CartStorageKey);
        var item = cart.FirstOrDefault(entry => entry.ProductId == productId);

        if (item is null)
        {
            return;
        }

        item.Quantity = quantity;
        await localStorageService.SetItemAsync(CartStorageKey, cart);
    }

    public async Task RemoveItemAsync(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            return;
        }

        await EnsureCartExistsAsync();
        var cart = await localStorageService.GetItemAsync<List<CartItem>>(CartStorageKey);
        var item = cart.FirstOrDefault(entry => entry.ProductId == productId);

        if (item is null)
        {
            return;
        }

        cart.Remove(item);
        await localStorageService.SetItemAsync(CartStorageKey, cart);
    }

    public async Task ClearAsync()
    {
        await localStorageService.SetItemAsync(CartStorageKey, new List<CartItem>());
    }

    public async Task<int> GetItemCountAsync()
    {
        var cart = await GetCartAsync();
        return cart.Sum(item => item.Quantity);
    }

    public async Task<decimal> GetTotalAsync()
    {
        var cart = await GetCartAsync();
        return cart.Sum(item => item.Price * item.Quantity);
    }

    private async Task EnsureCartExistsAsync()
    {
        if (!await localStorageService.ContainsKeyAsync(CartStorageKey))
        {
            await localStorageService.SetItemAsync(CartStorageKey, new List<CartItem>());
        }
    }
}
