using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Esclean.Models.Trays;
using Esclean.Services.Api.Trays;

namespace Esclean.ViewModels.Trays;

public partial class TrayMovementsViewModel : ViewModelBase
{
    private readonly ITrayApiService _trayApiService;
    
    // Catálogo de charolas
    [ObservableProperty]
    private ObservableCollection<TrayInfo> _trays = new();

    // Charola seleccionada
    [ObservableProperty]
    private TrayInfo? _selectedTray;

    // Inventario
    [ObservableProperty]
    private ObservableCollection<TrayInventoryInfo> _trayInventory = new();

    // Filtros
    [ObservableProperty]
    private string? _filterTrayId;

    [ObservableProperty]
    private string? _filterModel;

    // Ubicaciones
    [ObservableProperty]
    private ObservableCollection<string> _locations = new();

    [ObservableProperty]
    private string? _selectedLocation;
    
    [ObservableProperty]
    private TrayMovement _currentMovement = new();
    
    
}