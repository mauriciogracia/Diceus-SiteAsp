﻿using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RequestModels;

namespace SiteASP.Common
{
    public class ApiBroker
    {
        public static string _baseUri = "https://ContactsApi-mgg.azurewebsites.net/";
        private HttpClient _httpClient;

        public static ApiBroker prepare()
        {
            ApiBroker api = new ApiBroker();
            api._httpClient = new HttpClient();
            api._httpClient.BaseAddress = new Uri(_baseUri);

            return api;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            // Prepare the request data
            var user = new UserRequest { Username = username, Password = password };
            var jsonContent = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            bool isValid = false;

            // Make the HTTP POST request to the ValidateUser endpoint
            var response = await _httpClient.PostAsync("api/Users/validate", content);

            // Process the response
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                isValid = JsonConvert.DeserializeObject<bool>(responseContent);
            }
            // If the response is not successful, return false or handle the error accordingly
            return isValid;
        }

        public async Task<bool> CreateUserAsync(string username, string password)
        {
            // Prepare the request data
            var user = new UserRequest { Username = username, Password = password };
            var jsonContent = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            bool userCreated = false;

            // Make the HTTP POST request to the ValidateUser endpoint
            var response = await _httpClient.PostAsync("api/Users", content);

            // Process the response
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                userCreated = JsonConvert.DeserializeObject<bool>(responseContent);
            }
            // If the response is not successful, return false or handle the error accordingly
            return userCreated;
        }

        public async Task<string> StartSessionAsync(string userId)
        {
            string token = string.Empty;

            // Serialize the userId to JSON format
            var userIdJson = Newtonsoft.Json.JsonConvert.SerializeObject(userId);

            // Create the request content with the serialized userId
            var content = new StringContent(userIdJson, Encoding.UTF8, "application/json");

            try
            {
                // Send the POST request to the StartSession endpoint
                var response = await _httpClient.PostAsync("StartSession", content);

                // Check if the request was successful (HTTP status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content (the token)
                    token = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Request was not successful, handle the error
                    Console.WriteLine("Error: " + response.StatusCode + " - " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Exception occurred, handle the error
                Console.WriteLine("Exception: " + ex.Message);
            }
            return token;
        }
    }
}