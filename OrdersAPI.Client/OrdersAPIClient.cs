using DataContracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrdersAPI.Client
{
    public class OrdersAPIClient
    {
        private readonly HttpClient client;

        public OrdersAPIClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            this.client = client;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync(int userId)
        {
            HttpResponseMessage response = await client.GetAsync($"users/{userId}/orders").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IEnumerable<OrderDto>>().ConfigureAwait(false);
        }

        public async Task<OrderDto> GetOrderAsync(int userId, int orderId)
        {
            HttpResponseMessage response = await client.GetAsync($"users/{userId}/orders/{orderId}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<OrderDto>().ConfigureAwait(false);
        }

        public async Task<bool> SaveOrderAsync(int userId, OrderDto order)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync<OrderDto>($"users/{userId}/orders", order).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<bool>().ConfigureAwait(false);
        }

        public async Task<bool> DeleteOrderAsync(int userId, int orderId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"users/{userId}/orders/{orderId}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<bool>().ConfigureAwait(false);
        }

        public async Task<bool> DeleteOrdersAsync(int userId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"users/{userId}/orders").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<bool>().ConfigureAwait(false);
        }
    }
}
