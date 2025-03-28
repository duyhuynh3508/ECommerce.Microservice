using System.Text.Json;
using Azure;
using ECommerce.Microservice.OrderService.Api.Models;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using Polly;

namespace ECommerce.Microservice.OrderService.Api.Services
{
    public interface IProductService
    {
        Task<ProductModel> GetProductByID(int productID);
    }

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncPolicy _retryPolicy;
        private readonly AsyncPolicy _timeoutPolicy;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _retryPolicy = Policy.Handle<HttpRequestException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(3, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)));

            _timeoutPolicy = Policy.TimeoutAsync(5);
        }


        public async Task<ProductModel> GetProductByID(int productID)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    return await _timeoutPolicy.ExecuteAsync(async () =>
                    {
                        var response = await _httpClient.GetAsync($"api/product/getProductById?id={productID}");
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var product = JsonSerializer.Deserialize<ResponseResult>(content, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                            return (ProductModel)product.data;
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
