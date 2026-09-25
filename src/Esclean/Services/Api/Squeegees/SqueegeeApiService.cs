using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

using Esclean.Models.Squeegees;

namespace Esclean.Services.Api.Squeegees;

public class SqueegeeApiService : ISqueegeeApiService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public SqueegeeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    // ==========================================
    // Porta-navajas
    // ==========================================

    public async Task<List<SqueegeeInfo>> GetSqueegeesAsync()
    {
        var response = await _httpClient.GetAsync(
            "squeegee_holders?active=eq.true&order=id_holder_type");

        if (!response.IsSuccessStatusCode)
            return new List<SqueegeeInfo>();

        var squeegees =
            await response.Content.ReadFromJsonAsync<List<SqueegeeInfo>>(
                JsonOptions);

        return squeegees ?? new List<SqueegeeInfo>();
    }


    public async Task<SqueegeeInfo?> GetSqueegeeAsync(
        short idHolderType)
    {
        if (idHolderType <= 0)
            return null;

        var response = await _httpClient.GetAsync(
            $"squeegee_holders?id_holder_type=eq.{idHolderType}");

        if (!response.IsSuccessStatusCode)
            return null;

        var squeegees =
            await response.Content.ReadFromJsonAsync<List<SqueegeeInfo>>(
                JsonOptions);

        return squeegees is { Count: > 0 }
            ? squeegees[0]
            : null;
    }


    // ==========================================
    // Inventario de navajas
    // ==========================================

    public async Task<List<SqueegeeInventoryInfo>> GetInventoryAsync()
    {
        var response = await _httpClient.GetAsync(
            "squeegee_blades?active=eq.true&order=id_blade_type");

        if (!response.IsSuccessStatusCode)
            return new List<SqueegeeInventoryInfo>();

        var inventory =
            await response.Content.ReadFromJsonAsync<
                List<SqueegeeInventoryInfo>>(
                    JsonOptions);

        return inventory ?? new List<SqueegeeInventoryInfo>();
    }


    // ==========================================
    // Movimientos
    // ==========================================

    public async Task<List<SqueegeeMovement>> GetLastMovementsAsync(
        int limit = 20)
    {
        if (limit <= 0)
            limit = 20;

        var response = await _httpClient.GetAsync(
            $"squeegee_blade_movements" +
            $"?order=movement_at.desc&limit={limit}");

        if (!response.IsSuccessStatusCode)
            return new List<SqueegeeMovement>();

        var movements =
            await response.Content.ReadFromJsonAsync<
                List<SqueegeeMovement>>(
                    JsonOptions);

        return movements ?? new List<SqueegeeMovement>();
    }


    public async Task<bool> CreateMovementAsync(
        SqueegeeMovement movement)
    {
        if (movement is null)
            return false;

        var response = await _httpClient.PostAsJsonAsync(
            "squeegee_blade_movements",
            movement,
            JsonOptions);

        return response.IsSuccessStatusCode;
    }
}