using DataContracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace UsersAPI.Client
{
    public class UsersAPIClient
    {
        private readonly HttpClient client;

        public UsersAPIClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            this.client = client;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            HttpResponseMessage response = await client.GetAsync("users").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IEnumerable<UserDto>>().ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserNameDto>> GetUserNamesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("users/names").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IEnumerable<UserNameDto>>().ConfigureAwait(false);
        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            HttpResponseMessage response = await client.GetAsync($"users/{userId}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<UserDto>().ConfigureAwait(false);
        }

        public async Task SaveUserAsync(UserDto user)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync<UserDto>("users", user).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUserAsync(int userId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"users/{userId}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
