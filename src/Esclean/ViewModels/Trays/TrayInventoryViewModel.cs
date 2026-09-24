using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Esclean.Models.Trays;

namespace Esclean.ViewModels.Trays;

public partial class TrayInventoryViewModel : ViewModelBase
{
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

    // Buscar
    [RelayCommand]
    private void Search()
    {
        // Por ahora vacío.
        // Después aquí filtraremos TrayInventory.
    }
}