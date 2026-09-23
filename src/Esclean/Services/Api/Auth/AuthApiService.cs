using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Esclean.Models.Auth;

namespace Esclean.Services.Api.Auth;

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public AuthApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserSession?> LoginAsync(
        string employeeNumber,
        string password,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(employeeNumber))
            return null;

        if (string.IsNullOrWhiteSpace(password))
            return null;

        var request = new LoginRequest
        {
            EmployeeNumber = employeeNumber.Trim(),
            Password = password
        };

        try
        {
            Console.WriteLine("===== LOGIN REQUEST =====");

            Console.WriteLine($"BaseAddress: {_httpClient.BaseAddress}");
            Console.WriteLine($"Employee: {request.EmployeeNumber}");

            var requestJson = JsonSerializer.Serialize(
                request,
                JsonOptions);

            Console.WriteLine($"JSON: {requestJson}");

            Console.WriteLine("=========================");

            using var response = await _httpClient.PostAsJsonAsync(
                "rpc/login",
                request,
                JsonOptions,
                cancellationToken);

            Console.WriteLine($"HTTP Status: {(int)response.StatusCode}");
            Console.WriteLine($"HTTP Reason: {response.ReasonPhrase}");

            var responseBody = await response.Content.ReadAsStringAsync(
                cancellationToken);

            Console.WriteLine("===== LOGIN RESPONSE =====");
            Console.WriteLine(responseBody);
            Console.WriteLine("==========================");

            if (!response.IsSuccessStatusCode)
                return null;

            if (string.IsNullOrWhiteSpace(responseBody))
                return null;

            var users = JsonSerializer.Deserialize<UserSession[]>(
                responseBody,
                JsonOptions);

            if (users is null || users.Length == 0)
                return null;

            return users[0];
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("===== HTTP REQUEST ERROR =====");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine($"Inner: {ex.InnerException?.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            Console.WriteLine("==============================");

            throw;
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine("===== REQUEST CANCELED =====");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine("============================");

            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine("===== JSON ERROR =====");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine("======================");

            return null;
        }
    }
}