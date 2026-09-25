using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

using Esclean.Models.Trays;

namespace Esclean.Services.Api.Trays;

public class TrayApiService : ITrayApiService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public TrayApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TrayInventoryInfo>> GetTrayInventoryAsync()
    {
        // 1. Hacemos la petición a la ruta correspondiente de tu API (ejemplo: "tray_inventory")
        var response = await _httpClient.GetAsync("tray_inventory");

        // 2. Si la API falla, devolvemos una lista vacía para evitar que la app se caiga
        if (!response.IsSuccessStatusCode)
            return new List<TrayInventoryInfo>();

        // 3. Transformamos el JSON de la API en una lista de C#
        var inventory = await response.Content.ReadFromJsonAsync<List<TrayInventoryInfo>>(JsonOptions);

        // 4. Devolvemos el inventario o una lista vacía si venía nulo
        return inventory ?? new List<TrayInventoryInfo>();
    }

    public async Task<TrayInfo?> GetTrayInfoAsync(string idTray)
    {
        if (string.IsNullOrWhiteSpace(idTray))
            return null;

        var response = await _httpClient.GetAsync(
            $"trays?id_tray=eq.{Uri.EscapeDataString(idTray.Trim())}");

        if (!response.IsSuccessStatusCode)
            return null;

        var trays = await response.Content.ReadFromJsonAsync<List<TrayInfo>>(
            JsonOptions);

        return trays is { Count: > 0 } ? trays[0] : null;
    }

    public async Task<List<TrayInfo>> GetTraysAsync()
    {
        var response = await _httpClient.GetAsync(
            "trays?active=eq.true&order=id_tray");

        if (!response.IsSuccessStatusCode)
            return new List<TrayInfo>();

        var trays = await response.Content.ReadFromJsonAsync<List<TrayInfo>>(
            JsonOptions);

        return trays ?? new List<TrayInfo>();
    }

    public async Task<bool> CreateMovementAsync(TrayMovement movement)
    {
        if (movement is null)
            return false;

        var response = await _httpClient.PostAsJsonAsync(
            "tray_movements",
            movement,
            JsonOptions);

        return response.IsSuccessStatusCode;
    }
}