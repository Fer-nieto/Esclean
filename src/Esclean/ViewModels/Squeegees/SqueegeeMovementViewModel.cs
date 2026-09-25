using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Esclean.Models.Squeegees;
using Esclean.Services.Api.Squeegees;

namespace Esclean.ViewModels.Squeegees;

public partial class SqueegeeMovementViewModel : ViewModelBase
{
    // ==========================================
    // SERVICIO
    // ==========================================

    private readonly ISqueegeeApiService _apiService;


    // ==========================================
    // MOVIMIENTOS
    // ==========================================

    public ObservableCollection<SqueegeeMovement> Movements { get; }
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

    public SqueegeeMovementViewModel(
        ISqueegeeApiService apiService)
    {
        _apiService = apiService;
    }


    // ==========================================
    // CARGAR MOVIMIENTOS
    // ==========================================

    [RelayCommand]
    private async Task LoadMovementsAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            ErrorMessage = null;

            var movements =
                await _apiService.GetLastMovementsAsync();

            Movements.Clear();

            foreach (var movement in movements)
            {
                Movements.Add(movement);
            }
        }
        catch (System.Exception ex)
        {
            ErrorMessage =
                $"Error al cargar movimientos: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }


    // ==========================================
    // CREAR MOVIMIENTO
    // ==========================================

    public async Task<bool> CreateMovementAsync(
        SqueegeeMovement movement)
    {
        if (movement is null)
            return false;

        try
        {
            ErrorMessage = null;

            var success =
                await _apiService.CreateMovementAsync(movement);

            if (!success)
            {
                ErrorMessage =
                    "No fue posible registrar el movimiento.";

                return false;
            }

            await LoadMovementsAsync();

            return true;
        }
        catch (System.Exception ex)
        {
            ErrorMessage =
                $"Error al registrar movimiento: {ex.Message}";

            return false;
        }
    }
}