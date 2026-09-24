using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Esclean.Services.Api.Stencils;
using Esclean.Services.Session;

using Esclean.ViewModels.Dashboard;
using Esclean.ViewModels.Stencils;
using Esclean.ViewModels.Trays;
using Esclean.ViewModels.Squeegees;
using Esclean.ViewModels.Reports;
using Esclean.ViewModels.Settings;

namespace Esclean.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // SERVICIOS
    private readonly ISessionService _sessionService;

    private readonly IStencilApiService _stencilApiService;
    
    // EVENTOS
    public event Action? LogoutRequested;

    // USUARIO AUTENTICADO
    public string UserName =>
        _sessionService.CurrentUser?.Name
        ?? string.Empty;

    public string EmployeeNumber =>
        _sessionService.CurrentUser?.EmployeeNumber
        ?? string.Empty;

    public string RoleName =>
        _sessionService.CurrentUser?.RoleName
        ?? string.Empty;
    
    public string RoleCode =>
        _sessionService.CurrentUser?.RoleCode
        ?? string.Empty;

    public bool IsAuthenticated =>
        _sessionService.IsAuthenticated;
    
    // MÓDULOS
    public DashboardViewModel Dashboard { get; }

    public StencilViewModel Stencils { get; }

    public TrayViewModel Trays { get; }

    public SqueegeeViewModel Squeegees { get; }

    public ReportsViewModel Reports { get; }

    public SettingsViewModel Settings { get; }


    // VISTA ACTUAL
    [ObservableProperty]
    private ViewModelBase currentView;
    
    [ObservableProperty]
    private string currentSection = "DASHBOARD";
    
    // CONSTRUCTOR
    public MainViewModel(
        ISessionService sessionService)
    {
        _sessionService = sessionService;
        
        _stencilApiService = new MockStencilApiService();
        
        Dashboard = new DashboardViewModel();
        
        Stencils = new StencilViewModel(_stencilApiService);
        
        Trays = new TrayViewModel();
        
        Squeegees = new SqueegeeViewModel();
        
        Reports = new ReportsViewModel();
        
        Settings = new SettingsViewModel();

        currentView = Dashboard;
    }
    
    // DASHBOARD
    [RelayCommand]
    private void ShowDashboard()
    {
        CurrentView = Dashboard;
    }
    
    // STENCILES
    [RelayCommand]
    private void ShowStencils()
    {
        CurrentView = Stencils;
    }
    
    // CHAROLAS
    [RelayCommand]
    private void ShowTrays()
    {
        CurrentView = Trays;
    }
    
    // SQUEEGEES
    [RelayCommand]
    private void ShowSqueegees()
    {
        CurrentView = Squeegees;
    }
    
    // REPORTES
    [RelayCommand]
    private void ShowReports()
    {
        CurrentView = Reports;
    }
    
    // CONFIGURACIÓN
    [RelayCommand]
    private void ShowSettings()
    {
        CurrentView = Settings;
    }
    
    // LOGOUT
    [RelayCommand]
    private void Logout()
    {
        _sessionService.EndSession();
        LogoutRequested?.Invoke();
    }
}