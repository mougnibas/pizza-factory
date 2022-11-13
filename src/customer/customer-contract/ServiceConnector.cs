// Copyright(c) 2022 Yoann MOUGNIBAS
// 
// This file is part of PizzaFactory.
// 
// PizzaFactory is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// PizzaFactory is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with PizzaFactory.  If not, see <https://www.gnu.org/licenses/>.

using System.Text.Json;

namespace Mougnibas.PizzaFactory.Customer.Contract
{
    public sealed class ServiceConnector : IService, IDisposable
    {

        private readonly HttpClient httpClient;

        /// <summary>
        /// This constructor create a http client on it's own (bad practice).
        /// </summary>
        public ServiceConnector()
        {
            this.httpClient = new HttpClient();
        }

        /// <summary>
        /// This constructor require an injection of a http client.
        /// </summary>
        /// <param name="httpClient">The injected http client.</param>
        public ServiceConnector(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Pizza[] GetPizza()
        {
            // TODO This synchronous method is actually never tested.

            // URL to call
            Uri uri = new("http://localhost:5034/api/pizza");

            Pizza[]? result;
            using (HttpRequestMessage request = new(HttpMethod.Get, uri))
            {
                // Make a synchronous call to get a json result
                HttpResponseMessage response = httpClient.Send(request);
                _ = response.EnsureSuccessStatusCode();
                HttpContent content = response.Content;

                using (StreamReader reader = new(content.ReadAsStream()))
                {
                    // Get the json from the stream
                    string jsonString = reader.ReadToEnd();

                    // Deserialize the json
                    // We need to use this option to do it properly
                    JsonSerializerOptions options = new()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    result = JsonSerializer.Deserialize<Pizza[]>(jsonString, options);
                }

                // We could get a null result, but we can't return a null value
                result ??= Array.Empty<Pizza>();
            }

            // Return the result
            return result;
        }

        public async Task<Pizza[]> GetPizzaAsync()
        {
            // URL to call
            Uri uri = new("http://localhost/api/pizza");

            // Make an asynchronous call to get a json result
            string jsonString = await httpClient.GetStringAsync(uri).ConfigureAwait(false);

            // Deserialize the json
            // We need to use this option to do it properly
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            Pizza[]? result = JsonSerializer.Deserialize<Pizza[]>(jsonString, options);

            // We could get a null result, but we can't return a null value
            result ??= Array.Empty<Pizza>();

            // Return the result
            return result;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}