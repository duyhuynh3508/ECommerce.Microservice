using System.Text.Json;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using ECommerce.Microservice.UserService.Api.Models.User;
using Polly;

namespace ECommerce.Microservice.OrderService.Api.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserByID(int userID);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncPolicy _retryPolicy;
        private readonly AsyncPolicy _timeoutPolicy;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _retryPolicy = Policy.Handle<HttpRequestException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(3, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)));

            _timeoutPolicy = Policy.TimeoutAsync(5);
        }


        public async Task<UserModel> GetUserByID(int userID)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    return await _timeoutPolicy.ExecuteAsync(async () =>
                    {
                        var response = await _httpClient.GetAsync($"api/user/getUserById?id={userID}");
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var product = JsonSerializer.Deserialize<ResponseResult>(content, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                            return (UserModel)product.data;
                        }

                        return null;
                    });
                });
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return null;
            }
        }
    }
}
