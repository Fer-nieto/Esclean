using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Esclean.Models.Squeegees;
using Esclean.Services.Api.Squeegees;

namespace Esclean.ViewModels.Squeegees;

public partial class SqueegeeInventoryViewModel : ViewModelBase
{
    // ==========================================
    // SERVICIO
    // ==========================================

    private readonly ISqueegeeApiService _apiService;


    // ==========================================
    // INVENTARIO
    // ==========================================

    public ObservableCollection<SqueegeeInventoryInfo> Inventory { get; }
        = new();


    // ==========================================
    // ESTADO
    // ==========================================

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string? _errorMessage;


    // ==========================================
    // CONSTRUCTOR
    // ==========================================

    public SqueegeeInventoryViewModel(
        ISqueegeeApiService apiService)
    {
        _apiService = apiService;
    }


    // ==========================================
    // CARGAR INVENTARIO
    // ==========================================

    [RelayCommand]
    private async Task LoadInventoryAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            ErrorMessage = null;

            var inventory =
                await _apiService.GetInventoryAsync();

            Inventory.Clear();

            foreach (var item in inventory)
            {
                Inventory.Add(item);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage =
                $"Error al cargar inventario: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}