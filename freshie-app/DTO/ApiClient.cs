﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace freshie_app.DTO
{
    public class ApiClient
    {
        private static readonly HttpClient _client;

        static ApiClient()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://freshiehub.azurewebsites.net/") };
        }
        //REGISTRATION
        public static async Task<string> RegisterUser(User user)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/User/register", user);

            if (response.IsSuccessStatusCode)
            {
                return "Registration successful.";
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                return message;
            }
        }
        //LOGIN
        public static async Task<User> LoginUser(string email, string password)
        {
            User user = null;
            var loginModel = new { Email = email, Password = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("api/user/login", content);

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }
        //CHANGE USER INFO

        public static async Task<HttpResponseMessage> ChangeUserDetails(int id, UserUpdateModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"api/user/change-user-details/{id}";
            var response = await _client.PutAsync(url, data);

            return response;
        }
        //FRIDGE ITEMS
        public static async Task<List<Product>> GetUserProducts(int userId)
        {
            List<Product> userProducts = null;
            HttpResponseMessage response = await _client.GetAsync($"api/FridgeItems/products/{userId}");
            if (response.IsSuccessStatusCode)
            {
                userProducts = await response.Content.ReadAsAsync<List<Product>>();
            }
            return userProducts;
        }
        public static async Task<string> AddProduct(int id, Product product, DateOnly? expirationDate = null)
        {
            var item = new { UserId = id, Product = product, ExpirationDate = expirationDate };
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("api/fridgeitems", content);

            if (response.IsSuccessStatusCode)
            {
                return "Product added successfully.";
            }
            else
            {
                return $"Failed to add product. Status code: {response.StatusCode}";
            }
        }
        public static async Task<string> DeleteProduct(int id, Product product)
        {
            var item = new { UserId = id, Product = product };
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("api/fridgeitems", UriKind.Relative),
                Content = content
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return "Product deleted successfully.";
            }
            else
            {
                return $"Failed to delete product. Status code: {response.StatusCode}";
            }
        }
        public static async Task<DateOnly?> GetExpirationDate(int id, Product product)
        {
            DateOnly? expirationDate = null;
            HttpResponseMessage response = await _client.GetAsync($"api/fridgeitems/{id}");
            if (response.IsSuccessStatusCode)
            {
                var fridgeItems = await response.Content.ReadAsAsync<List<FridgeItem>>();
                if (fridgeItems != null && product != null)
                {
                    var item = fridgeItems.FirstOrDefault(p => p.ProductId == product.ProductId);
                    if (item != null)
                    {
                        expirationDate = item.ExpirationDate;
                    }
                }
            }
            return expirationDate;
        }
        public static async Task<List<FridgeItem>> GetFridgeItems(int userId)
        {
            List<FridgeItem> fridgeItems = null;
            HttpResponseMessage response = await _client.GetAsync($"api/FridgeItems/{userId}");
            if (response.IsSuccessStatusCode)
            {
                fridgeItems = await response.Content.ReadAsAsync<List<FridgeItem>>();
            }
            return fridgeItems;
        }
        public static async Task ChangeExpirationDate(FridgeItem fridgeItem)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(
                $"api/FridgeItems", fridgeItem);
            response.EnsureSuccessStatusCode();
        }
        public static async Task<List<Product>> SortByExpirationDate(int userId)
        {
            var userFridgeItems = await GetFridgeItems(userId);
            var userProducts = await GetUserProducts(userId);

            var productLookup = userProducts.ToDictionary(p => p.ProductId);
            var sortedUserItems = userFridgeItems.OrderBy(p => p.ExpirationDate);
            var sortedUserProducts = new List<Product>();

            foreach (var item in sortedUserItems)
            {
                if (productLookup.TryGetValue((int)item.ProductId, out var product))
                {
                    sortedUserProducts.Add(product);
                }
            }

            return sortedUserProducts;
        }

        //GROCERIES LIST
        public static async Task<List<Product>> GetUserGroceries(int userId)
        {
            List<Product> userGroceries = null;
            HttpResponseMessage response = await _client.GetAsync($"api/GroceriesLists/{userId}");
            if (response.IsSuccessStatusCode)
            {
                userGroceries = await response.Content.ReadAsAsync<List<Product>>();
            }
            return userGroceries;
        }
        public static async Task<string> AddGroceriesItem(int id, Product product)
        {
            var item = new { UserId = id, Product = product };
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("api/GroceriesLists", content);

            if (response.IsSuccessStatusCode)
            {
                return "Product added successfully.";
            }
            else
            {
                return $"Failed to add product. Status code: {response.StatusCode}";
            }
        }
        public static async Task<string> DeleteGroceries(int id, Product product)
        {
            var item = new { UserId = id, Product = product };
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("api/GroceriesLists", UriKind.Relative),
                Content = content
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return "Product deleted successfully.";
            }
            else
            {
                return $"Failed to delete product. Status code: {response.StatusCode}";
            }
        }
        //PRODUCTS
        public static async Task<List<Product>> GetAllProducts()
        {
            List<Product> allProducts = null;
            HttpResponseMessage response = await _client.GetAsync("api/Products");
            if (response.IsSuccessStatusCode)
            {
                allProducts = await response.Content.ReadAsAsync<List<Product>>();
            }
            return allProducts;
        }
    }
}
