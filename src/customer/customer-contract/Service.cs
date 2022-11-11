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
    public class Service : IService
    {
        public Pizza[] get()
        {
            // A simple http client
            HttpClient httpClient = new HttpClient();

            // URL to call
            string uri = "http://localhost:5034/api/pizza";

            // Make a synchronous call to get a json result
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = httpClient.Send(request);
            response.EnsureSuccessStatusCode();
            HttpContent content = response.Content;
            StreamReader reader = new(content.ReadAsStream());
            string jsonString = reader.ReadToEnd();

            // Deserialize the json
            // We need to use this option to do it properly
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            Pizza[] result = JsonSerializer.Deserialize<Pizza[]>(jsonString, options);

            // Return the result
            return result;
        }
    }
}