using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Esclean.Models.Stencils;

namespace Esclean.Services.Api.Stencils;

public class StencilApiService : IStencilApiService
{
    private readonly HttpClient _httpClient;


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public StencilApiService(
        HttpClient httpClient)
    {
        _httpClient =
            httpClient;
    }


    // =========================================================
    // CONSULTAR STENCIL
    // =========================================================

    public async Task<StencilInfo?> GetStencilAsync(
        string steelNo)
    {
        try
        {
            return await _httpClient
                .GetFromJsonAsync<StencilInfo>(
                    $"stencils/{steelNo}");
        }
        catch
        {
            return null;
        }
    }


    // =========================================================
    // ÚLTIMOS MOVIMIENTOS
    // =========================================================

    public async Task<List<StencilMovement>>
        GetLastMovementsAsync(
            int limit = 20)
    {
        try
        {
            var movements =
                await _httpClient
                    .GetFromJsonAsync<List<StencilMovement>>(
                        $"stencil-movements?limit={limit}");

            return movements ?? [];
        }
        catch
        {
            return [];
        }
    }


    // =========================================================
    // REGISTRAR MOVIMIENTO
    // =========================================================

    public async Task<bool> CreateMovementAsync(
        StencilMovement movement)
    {
        try
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "stencil-movements",
                    movement);

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }


    // =========================================================
    // ÚLTIMOS REPORTES DE DAÑO
    // =========================================================

    public async Task<List<StencilDamageReport>>
        GetLastDamageReportsAsync(
            int limit = 20)
    {
        try
        {
            var reports =
                await _httpClient
                    .GetFromJsonAsync<List<StencilDamageReport>>(
                        $"stencil-damage-reports?limit={limit}");

            return reports ?? [];
        }
        catch
        {
            return [];
        }
    }


    // =========================================================
    // REGISTRAR REPORTE DE DAÑO
    // =========================================================

    public async Task<bool> CreateDamageReportAsync(
        StencilDamageReport report)
    {
        try
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "stencil-damage-reports",
                    report);

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}