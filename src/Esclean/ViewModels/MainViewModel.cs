using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Esclean.ViewModels.Dashboard;
using Esclean.ViewModels.Reports;
using Esclean.ViewModels.Settings;
using Esclean.ViewModels.Squeegees;
using Esclean.ViewModels.Stencils;
using Esclean.ViewModels.Trays;

namespace Esclean.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public DashboardViewModel Dashboard { get; }

    public StencilViewModel Stencils { get; }

    public TrayViewModel Trays { get; }

    public SqueegeeViewModel Squeegees { get; }

    public ReportsViewModel Reports { get; }

    public SettingsViewModel Settings { get; }


    [ObservableProperty]
    private ViewModelBase currentView;


    [ObservableProperty]
    private string currentSection = "Dashboard";


    public MainViewModel()
    {
        Dashboard = new DashboardViewModel();

        Stencils = new StencilViewModel();

        Trays = new TrayViewModel();

        Squeegees = new SqueegeeViewModel();

        Reports = new ReportsViewModel();

        Settings = new SettingsViewModel();

        CurrentView = Dashboard;
    }


    [RelayCommand]
    private void ShowDashboard()
    {
        CurrentView = Dashboard;
        CurrentSection = "Dashboard";
    }


    [RelayCommand]
    private void ShowStencils()
    {
        CurrentView = Stencils;
        CurrentSection = "Stencils";
    }


    [RelayCommand]
    private void ShowTrays()
    {
        CurrentView = Trays;
        CurrentSection = "Trays";
    }


    [RelayCommand]
    private void ShowSqueegees()
    {
        CurrentView = Squeegees;
        CurrentSection = "Squeegees";
    }


    [RelayCommand]
    private void ShowReports()
    {
        CurrentView = Reports;
        CurrentSection = "Reports";
    }


    [RelayCommand]
    private void ShowSettings()
    {
        CurrentView = Settings;
        CurrentSection = "Settings";
    }
}