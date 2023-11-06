using freshie_DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace tester_web_api
{
    internal class UserTester
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        const string api_url = "https://freshie-webapi.azurewebsites.net/api/";

        public UserTester()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<User> Login(string email, string password)
        {

            Uri uri = new Uri(string.Format(api_url + $"User/login/{email}/{password}", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<User>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return null;
        }

    }
}
