using System.Collections.Generic;
using System.Threading.Tasks;
using Esclean.Models.Stencils;

namespace Esclean.Services.Api.Stencils;

public interface IStencilApiService
{
    Task<StencilInfo?> GetStencilAsync(
        string steelNo);

    Task<List<StencilMovement>> GetLastMovementsAsync(
        int limit = 20);

    Task<bool> CreateMovementAsync(
        StencilMovement movement);


    // =========================================================
    // REPORTES DE DAÑO
    // =========================================================

    Task<List<StencilDamageReport>> GetLastDamageReportsAsync(
        int limit = 20);

    Task<bool> CreateDamageReportAsync(
        StencilDamageReport report);
}