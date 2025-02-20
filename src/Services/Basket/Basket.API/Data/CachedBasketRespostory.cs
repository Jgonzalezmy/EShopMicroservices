﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRespostory(IBasketRespostory respostory, IDistributedCache cache) : IBasketRespostory
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await respostory.DeleteBasket(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(cacheBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;

            var basket = await respostory.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await respostory.StoreBasket(basket, cancellationToken);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }
    }
}
