using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Esclean.ViewModels.Trays;

public class TraysViewModel:ViewModelBase
{
    public object? CurrentTrayView { get; private set; }

    public ICommand ShowInventoryCommand { get; }

    public ICommand ShowMovementsCommand { get; }

    public ICommand ShowCatalogCommand { get; }

    public TraysViewModel()
    {
        ShowInventoryCommand = new RelayCommand(ShowInventory);
        ShowMovementsCommand = new RelayCommand(ShowMovements);
        ShowCatalogCommand = new RelayCommand(ShowCatalog);

        ShowInventory();
    }

    private void ShowInventory()
    {
        CurrentTrayView = null;

        // Posteriormente:
        // CurrentTrayView = new TrayInventoryViewModel(...);
    }

    private void ShowMovements()
    {
        CurrentTrayView = null;

        // Posteriormente:
        // CurrentTrayView = new TrayMovementsViewModel(...);
    }

    private void ShowCatalog()
    {
        CurrentTrayView = null;

        // Posteriormente:
        // CurrentTrayView = new TrayCatalogViewModel(...);
    }
}