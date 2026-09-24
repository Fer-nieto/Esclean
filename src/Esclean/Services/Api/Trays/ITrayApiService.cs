using System.Collections.Generic;
using System.Threading.Tasks;
using Esclean.Models.Trays;

namespace Esclean.Services.Api.Trays;

public interface ITrayApiService
{
    
    //Inventario
    Task<List<TrayInventoryInfo>> GetTrayInventoryAsync();
    
    // Movimientos
    Task<TrayInfo?> GetTrayInfoAsync(string idTray);

    Task<List<TrayInfo>> GetTraysAsync();

    Task<bool> CreateMovementAsync(TrayMovement movement);
    
    
}

