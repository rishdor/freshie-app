using Newtonsoft.Json;
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
            _client = new HttpClient { BaseAddress = new Uri("https://freshie-api.azurewebsites.net/") };
        }

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
        //public static async Task<List<>>

    }
}
