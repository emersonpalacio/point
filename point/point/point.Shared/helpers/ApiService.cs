using Newtonsoft.Json;
using point.models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace point.helpers
{
    public class ApiService
    {
        public static async Task<Response> LoginAsync( LoginRequest model ) 
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:44392/")
                };

                HttpResponseMessage response = await client.PostAsync("api/Account/Login", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSucces = false,
                        Message = result
                    };
                }

                User user = JsonConvert.DeserializeObject<User>(result);
                return new Response
                {
                    IsSucces = true,
                    Result = user
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucces = false,
                    Message = ex.Message
                };
            }
        }
    }
}
