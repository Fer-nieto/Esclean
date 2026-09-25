using System.Collections.Generic;
using System.Threading.Tasks;
using Esclean.Models.Squeegees;

namespace Esclean.Services.Api.Squeegees;

public interface ISqueegeeApiService
{
    // =========================
    // Porta-navajas
    // =========================

    Task<List<SqueegeeInfo>> GetSqueegeesAsync();

    Task<SqueegeeInfo?> GetSqueegeeAsync(
        short idHolderType);


    // =========================
    // Inventario de navajas
    // =========================

    Task<List<SqueegeeInventoryInfo>> GetInventoryAsync();


    // =========================
    // Movimientos de navajas
    // =========================

    Task<List<SqueegeeMovement>> GetLastMovementsAsync(
        int limit = 20);

    Task<bool> CreateMovementAsync(
        SqueegeeMovement movement);
}