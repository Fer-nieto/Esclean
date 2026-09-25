using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Esclean.Services.Api.Squeegees;

namespace Esclean.ViewModels.Squeegees;

public partial class SqueegeeViewModel : ViewModelBase
{
    // ==========================================
    // SERVICIO
    // ==========================================

    private readonly ISqueegeeApiService _apiService;


    // ==========================================
    // SUBVISTAS
    // ==========================================

    private readonly SqueegeeInventoryViewModel _inventoryViewModel;

    private readonly SqueegeeMovementViewModel _movementViewModel;


    // ==========================================
    // VISTA ACTUAL
    // ==========================================

    [ObservableProperty]
    private ViewModelBase _currentSqueegeeView;


    // ==========================================
    // CONSTRUCTOR
    // ==========================================

    public SqueegeeViewModel(
        ISqueegeeApiService apiService)
    {
        _apiService = apiService;

        _inventoryViewModel =
            new SqueegeeInventoryViewModel(_apiService);

        _movementViewModel =
            new SqueegeeMovementViewModel(_apiService);

        _currentSqueegeeView =
            _inventoryViewModel;
    }


    // ==========================================
    // INVENTARIO
    // ==========================================

    [RelayCommand]
    private void ShowInventory()
    {
        CurrentSqueegeeView =
            _inventoryViewModel;
    }


    // ==========================================
    // MOVIMIENTOS
    // ==========================================

    [RelayCommand]
    private void ShowMovements()
    {
        CurrentSqueegeeView =
            _movementViewModel;
    }
}