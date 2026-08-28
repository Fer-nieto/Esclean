using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Esclean.Models.Stencils;
using Esclean.Services.Api.Stencils;

namespace Esclean.ViewModels.Stencils;

public partial class StencilDeliveryViewModel : ViewModelBase
{
    private readonly IStencilApiService _apiService;


    // =========================================================
    // CAMPOS DEL FORMULARIO
    // =========================================================

    [ObservableProperty]
    private string steelNo = string.Empty;

    [ObservableProperty]
    private string requisitor = string.Empty;

    [ObservableProperty]
    private string selectedLine = string.Empty;

    [ObservableProperty]
    private string comments = string.Empty;


    // =========================================================
    // MOVIMIENTO
    // =========================================================

    [ObservableProperty]
    private string movementType = "OUT";


    // =========================================================
    // INFORMACIÓN DEL STENCIL
    // =========================================================

    [ObservableProperty]
    private string stencilModel = "-";

    [ObservableProperty]
    private string stencilSide = "-";

    [ObservableProperty]
    private string currentLocation = "-";

    [ObservableProperty]
    private string stencilStatus = "-";


    // =========================================================
    // ESTADO UI
    // =========================================================

    [ObservableProperty]
    private string message = "Listo para registrar movimiento.";

    [ObservableProperty]
    private bool isBusy;


    // =========================================================
    // CATÁLOGOS
    // =========================================================

    public ObservableCollection<string> Lines { get; } =
    [
        "AG01",
        "AG02",
        "AG03",
        "AG04",
        "AG05",
        "AG06",
        "AG07",
        "AG08"
    ];


    // =========================================================
    // TABLA
    // =========================================================

    public ObservableCollection<StencilMovement> LastMovements { get; }
        = [];


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public StencilDeliveryViewModel(
        IStencilApiService apiService)
    {
        _apiService = apiService;

        SelectedLine = "AG01";
    }


    // =========================================================
    // INICIALIZACIÓN
    // =========================================================

    public async Task InitializeAsync()
    {
        await LoadLastMovementsAsync();
    }


    // =========================================================
    // CONSULTAR STENCIL
    // =========================================================

    [RelayCommand]
    private async Task SearchStencilAsync()
    {
        if (string.IsNullOrWhiteSpace(SteelNo))
        {
            Message = "Ingrese un Steel No.";
            return;
        }

        try
        {
            IsBusy = true;

            var stencil =
                await _apiService.GetStencilAsync(
                    SteelNo.Trim());

            if (stencil is null)
            {
                ClearStencilInfo();

                Message = "Stencil no encontrado.";

                return;
            }

            StencilModel = stencil.Model;
            StencilSide = stencil.Side;
            CurrentLocation = stencil.CurrentLocation;
            StencilStatus = stencil.Status;

            Message = "Stencil encontrado.";
        }
        finally
        {
            IsBusy = false;
        }
    }


    // =========================================================
    // SELECCIONAR SALIDA
    // =========================================================

    [RelayCommand]
    private void SelectOut()
    {
        MovementType = "OUT";

        Message = "Movimiento seleccionado: SALIDA.";
    }


    // =========================================================
    // SELECCIONAR ENTRADA
    // =========================================================

    [RelayCommand]
    private void SelectIn()
    {
        MovementType = "IN";

        Message = "Movimiento seleccionado: ENTRADA.";
    }


    // =========================================================
    // CONFIRMAR MOVIMIENTO
    // =========================================================

    [RelayCommand]
    private async Task ConfirmAsync()
    {
        if (!ValidateForm())
            return;

        try
        {
            IsBusy = true;

            var movement =
                new StencilMovement
                {
                    SteelNo = SteelNo.Trim(),

                    Requisitor = Requisitor.Trim(),

                    Line = SelectedLine,

                    MovementType = MovementType,

                    Comments = Comments.Trim(),

                    Date = DateTime.Now
                };


            var success =
                await _apiService.CreateMovementAsync(
                    movement);


            if (!success)
            {
                Message =
                    "No fue posible registrar el movimiento.";

                return;
            }


            Message =
                "Movimiento registrado correctamente.";


            ClearForm();


            await LoadLastMovementsAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }


    // =========================================================
    // CARGAR ÚLTIMOS MOVIMIENTOS
    // =========================================================

    [RelayCommand]
    private async Task LoadLastMovementsAsync()
    {
        try
        {
            IsBusy = true;

            var movements =
                await _apiService.GetLastMovementsAsync(20);


            LastMovements.Clear();


            foreach (var movement in movements)
            {
                LastMovements.Add(movement);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }


    // =========================================================
    // VALIDACIONES
    // =========================================================

    private bool ValidateForm()
    {
        if (string.IsNullOrWhiteSpace(SteelNo))
        {
            Message = "Ingrese el Steel No.";

            return false;
        }


        if (string.IsNullOrWhiteSpace(Requisitor))
        {
            Message =
                "Ingrese el número de empleado del requisitor.";

            return false;
        }


        if (string.IsNullOrWhiteSpace(SelectedLine))
        {
            Message =
                "Seleccione una línea.";

            return false;
        }


        return true;
    }


    // =========================================================
    // LIMPIAR
    // =========================================================

    private void ClearForm()
    {
        SteelNo = string.Empty;

        Requisitor = string.Empty;

        Comments = string.Empty;

        ClearStencilInfo();
    }


    private void ClearStencilInfo()
    {
        StencilModel = "-";

        StencilSide = "-";

        CurrentLocation = "-";

        StencilStatus = "-";
    }
}