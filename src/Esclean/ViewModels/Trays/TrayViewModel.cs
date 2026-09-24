using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Esclean.ViewModels.Trays;

public partial class TrayViewModel : ViewModelBase
{
    // SUBVISTAS

    private readonly TrayInventoryViewModel _inventoryViewModel;
    private readonly TrayMovementsViewModel _movementsViewModel;
    private readonly TrayCatalogViewModel _catalogViewModel;

    // VISTA ACTUAL

    [ObservableProperty]
    private ViewModelBase _currentTrayView;

    // CONSTRUCTOR

    public TrayViewModel()
    {
        _movementsViewModel = new TrayMovementsViewModel();
        _inventoryViewModel = new TrayInventoryViewModel();
        _catalogViewModel = new TrayCatalogViewModel();

        _currentTrayView = _movementsViewModel;
    }

    // MOVIMIENTOS
    [RelayCommand]
    private void ShowMovements()
    {
        CurrentTrayView = _movementsViewModel;
    }
    
    // INVENTARIO
    [RelayCommand]
    private void ShowInventory()
    {
        CurrentTrayView = _inventoryViewModel;
    }
    
    // CATÁLOGO
    [RelayCommand]
    private void ShowCatalog()
    {
        CurrentTrayView = _catalogViewModel;
    }
}