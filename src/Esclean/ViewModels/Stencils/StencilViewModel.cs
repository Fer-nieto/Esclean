using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Esclean.Services.Api.Stencils;

namespace Esclean.ViewModels.Stencils;

public partial class StencilViewModel : ViewModelBase
{
    // =========================================================
    // SERVICIO
    // =========================================================

    private readonly IStencilApiService _apiService;


    // =========================================================
    // SUBVISTAS
    // =========================================================

    private readonly StencilDeliveryViewModel _deliveryViewModel;

    private readonly StencilDamageReportViewModel _damageReportViewModel;

    private readonly StencilInventoryViewModel _inventoryViewModel;


    // =========================================================
    // VISTA ACTUAL
    // =========================================================

    [ObservableProperty]
    private ViewModelBase currentStencilView;


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public StencilViewModel(
        IStencilApiService apiService)
    {
        _apiService =
            apiService;


        _deliveryViewModel =
            new StencilDeliveryViewModel(
                _apiService);


        _damageReportViewModel =
            new StencilDamageReportViewModel(
                _apiService);


        _inventoryViewModel =
            new StencilInventoryViewModel();


        currentStencilView =
            _deliveryViewModel;
    }


    // =========================================================
    // ENTREGAS
    // =========================================================

    [RelayCommand]
    private void ShowDelivery()
    {
        CurrentStencilView =
            _deliveryViewModel;
    }


    // =========================================================
    // DAÑOS
    // =========================================================

    [RelayCommand]
    private void ShowDamageReport()
    {
        CurrentStencilView =
            _damageReportViewModel;
    }


    // =========================================================
    // INVENTARIO
    // =========================================================

    [RelayCommand]
    private void ShowInventory()
    {
        CurrentStencilView =
            _inventoryViewModel;
    }
}