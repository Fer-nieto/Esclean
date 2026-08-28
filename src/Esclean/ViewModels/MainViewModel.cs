using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Esclean.Services.Api.Stencils;

using Esclean.ViewModels.Dashboard;
using Esclean.ViewModels.Stencils;
using Esclean.ViewModels.Trays;
using Esclean.ViewModels.Squeegees;
using Esclean.ViewModels.Reports;
using Esclean.ViewModels.Settings;

namespace Esclean.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // =========================================================
    // SERVICIOS
    // =========================================================

    private readonly IStencilApiService _stencilApiService;


    // =========================================================
    // MÓDULOS
    // =========================================================

    public DashboardViewModel Dashboard { get; }

    public StencilViewModel Stencils { get; }

    public TrayViewModel Trays { get; }

    public SqueegeeViewModel Squeegees { get; }

    public ReportsViewModel Reports { get; }

    public SettingsViewModel Settings { get; }


    // =========================================================
    // VISTA ACTUAL
    // =========================================================

    [ObservableProperty]
    private ViewModelBase currentView;


    [ObservableProperty]
    private string currentSection =
        "DASHBOARD";


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public MainViewModel()
    {
        // =====================================================
        // SERVICIO TEMPORAL
        // =====================================================
        //
        // Actualmente utilizamos un servicio Mock para poder
        // desarrollar y validar la interfaz sin PostgreSQL.
        //
        // Flujo futuro:
        //
        // Avalonia
        //      ↓
        // StencilApiService
        //      ↓
        // HTTP
        //      ↓
        // PostgREST
        //      ↓
        // PostgreSQL
        //
        // =====================================================

        _stencilApiService =
            new MockStencilApiService();


        // =====================================================
        // CREAR MÓDULOS
        // =====================================================

        Dashboard =
            new DashboardViewModel();


        Stencils =
            new StencilViewModel(
                _stencilApiService);


        Trays =
            new TrayViewModel();


        Squeegees =
            new SqueegeeViewModel();


        Reports =
            new ReportsViewModel();


        Settings =
            new SettingsViewModel();


        // =====================================================
        // VISTA INICIAL
        // =====================================================

        currentView =
            Dashboard;
    }


    // =========================================================
    // DASHBOARD
    // =========================================================

    [RelayCommand]
    private void ShowDashboard()
    {
        CurrentView =
            Dashboard;

        CurrentSection =
            "DASHBOARD";
    }


    // =========================================================
    // STENCILES
    // =========================================================

    [RelayCommand]
    private void ShowStencils()
    {
        CurrentView =
            Stencils;

        CurrentSection =
            "STENCILES";
    }


    // =========================================================
    // CHAROLAS
    // =========================================================

    [RelayCommand]
    private void ShowTrays()
    {
        CurrentView =
            Trays;

        CurrentSection =
            "CHAROLAS";
    }


    // =========================================================
    // SQUEEGEES
    // =========================================================

    [RelayCommand]
    private void ShowSqueegees()
    {
        CurrentView =
            Squeegees;

        CurrentSection =
            "SQUEEGEES";
    }


    // =========================================================
    // REPORTES
    // =========================================================

    [RelayCommand]
    private void ShowReports()
    {
        CurrentView =
            Reports;

        CurrentSection =
            "REPORTES";
    }


    // =========================================================
    // CONFIGURACIÓN
    // =========================================================

    [RelayCommand]
    private void ShowSettings()
    {
        CurrentView =
            Settings;

        CurrentSection =
            "CONFIGURACIÓN";
    }
}