using IFS.ApiTests.Helpers;
using System.Net;
using System.Text;
using System.Text.Json;

namespace IFS.ApiTests.Clients
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private string BuildUrl(string endpoint) => $"{_baseUrl}/{endpoint.TrimStart('/')}";

        public ApiClient()
        {
            var settings = ConfigurationHelper.GetSettings();
            _baseUrl = settings.BaseUrl;
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds)
            };
        }

        public async Task<(HttpStatusCode StatusCode, T? Body)> GetAsync<T>(string endpoint)
        {
            var url = BuildUrl(endpoint);
            Console.WriteLine($"[GET] {url}");

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[RESPONSE] {(int)response.StatusCode}: {content}");

            if (!response.IsSuccessStatusCode)
                return (response.StatusCode, default);

            var body = JsonSerializer.Deserialize<T>(content, JsonOptions);
            return (response.StatusCode, body);
        }

        public async Task<(HttpStatusCode StatusCode, T? Body)> PostAsync<T>(string endpoint, object payload)
        {
            var url = BuildUrl(endpoint);
            var json = JsonSerializer.Serialize(payload, JsonOptions);
            Console.WriteLine($"[POST] {url} | Body: {json}");

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[RESPONSE] {(int)response.StatusCode}: {responseBody}");

            var body = JsonSerializer.Deserialize<T>(responseBody, JsonOptions);
            return (response.StatusCode, body);
        }

        public async Task<(HttpStatusCode StatusCode, T? Body)> PutAsync<T>(string endpoint, object payload)
        {
            var url = BuildUrl(endpoint);
            var json = JsonSerializer.Serialize(payload, JsonOptions);
            Console.WriteLine($"[PUT] {url} | Body: {json}");

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[RESPONSE] {(int)response.StatusCode}: {responseBody}");

            var body = JsonSerializer.Deserialize<T>(responseBody, JsonOptions);
            return (response.StatusCode, body);
        }

        public async Task<HttpStatusCode> DeleteAsync(string endpoint)
        {
            var url = BuildUrl(endpoint);
            Console.WriteLine($"[DELETE] {url}");

            var response = await _httpClient.DeleteAsync(url);
            Console.WriteLine($"[RESPONSE] {(int)response.StatusCode}");

            return response.StatusCode;
        }

        public void Dispose() => _httpClient.Dispose();
    }
}
