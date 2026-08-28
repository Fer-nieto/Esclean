using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Esclean.Models.Stencils;
using Esclean.Services.Api.Stencils;

namespace Esclean.ViewModels.Stencils;

public partial class StencilDamageReportViewModel : ViewModelBase
{
    private readonly IStencilApiService _apiService;

    [ObservableProperty]
    private string steelNo = string.Empty;

    [ObservableProperty]
    private string reporter = string.Empty;

    [ObservableProperty]
    private string selectedLine = "AG01";

    [ObservableProperty]
    private string selectedDamageType = "DAÑO EN MALLA";

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private string stencilModel = "-";

    [ObservableProperty]
    private string stencilSide = "-";

    [ObservableProperty]
    private string currentLocation = "-";

    [ObservableProperty]
    private string stencilStatus = "-";

    [ObservableProperty]
    private string message = "Listo para registrar reporte.";

    [ObservableProperty]
    private bool isBusy;

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

    public ObservableCollection<string> DamageTypes { get; } =
    [
        "DAÑO EN MALLA",
        "APERTURA OBSTRUIDA",
        "PERFORACIÓN",
        "TENSIÓN",
        "MARCO",
        "DEFORMACIÓN",
        "CONTAMINACIÓN",
        "OTRO"
    ];

    public ObservableCollection<StencilDamageReport> LastDamageReports { get; }
        = [];

    public StencilDamageReportViewModel(
        IStencilApiService apiService)
    {
        _apiService = apiService;
    }

    // =========================================================
    // INICIALIZACIÓN DE LA VISTA
    // =========================================================

    public async Task InitializeAsync()
    {
        await LoadDamageReportsAsync();
    }

    // =========================================================
    // BUSCAR STENCIL
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

            Message = "Stencil encontrado correctamente.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    // =========================================================
    // REGISTRAR REPORTE
    // =========================================================

    [RelayCommand]
    private async Task ConfirmAsync()
    {
        if (!ValidateForm())
        {
            return;
        }

        try
        {
            IsBusy = true;

            var report =
                new StencilDamageReport
                {
                    SteelNo = SteelNo.Trim(),
                    Model = StencilModel,
                    Side = StencilSide,
                    Line = SelectedLine,
                    Reporter = Reporter.Trim(),
                    DamageType = SelectedDamageType,
                    Description = Description.Trim()
                };

            var success =
                await _apiService.CreateDamageReportAsync(
                    report);

            if (!success)
            {
                Message =
                    "No fue posible registrar el reporte.";

                return;
            }

            ClearForm();

            await LoadDamageReportsAsync();

            Message =
                "Reporte registrado correctamente.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    // =========================================================
    // CARGAR ÚLTIMOS REPORTES
    // =========================================================

    [RelayCommand]
    private async Task LoadDamageReportsAsync()
    {
        try
        {
            IsBusy = true;

            var reports =
                await _apiService.GetLastDamageReportsAsync(20);

            LastDamageReports.Clear();

            foreach (var report in reports)
            {
                LastDamageReports.Add(report);
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

        if (string.IsNullOrWhiteSpace(Reporter))
        {
            Message = "Ingrese el número de empleado.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(SelectedLine))
        {
            Message = "Seleccione una línea.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(SelectedDamageType))
        {
            Message = "Seleccione un tipo de daño.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Description))
        {
            Message = "Describa el daño encontrado.";
            return false;
        }

        return true;
    }

    // =========================================================
    // LIMPIEZA
    // =========================================================

    private void ClearForm()
    {
        SteelNo = string.Empty;
        Reporter = string.Empty;
        Description = string.Empty;

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