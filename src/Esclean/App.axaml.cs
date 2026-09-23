using System;
using System.Net.Http;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Esclean.Services.Api.Auth;
using Esclean.Services.Session;

using Esclean.ViewModels;
using Esclean.ViewModels.Auth;

using Esclean.Views;
using Esclean.Views.Auth;

namespace Esclean;

public partial class App : Application
{
    private HttpClient? _httpClient;
    private ISessionService? _sessionService;
    private IAuthApiService? _authApiService;


    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }


    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // =====================================================
            // HTTP CLIENT
            // =====================================================

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(
                    "http://localhost:3000/"
                )
            };


            // =====================================================
            // SERVICIOS
            // =====================================================

            _sessionService = new SessionService();

            _authApiService = new AuthApiService(
                _httpClient
            );


            // =====================================================
            // MOSTRAR LOGIN INICIAL
            // =====================================================

            ShowLoginWindow(
                desktop
            );
        }

        base.OnFrameworkInitializationCompleted();
    }


    // =========================================================
    // MOSTRAR LOGIN
    // =========================================================

    private void ShowLoginWindow(
        IClassicDesktopStyleApplicationLifetime desktop)
    {
        if (_authApiService is null ||
            _sessionService is null)
        {
            return;
        }


        // =====================================================
        // LOGIN VIEWMODEL
        // =====================================================

        var loginViewModel = new LoginViewModel(
            _authApiService,
            _sessionService
        );


        // =====================================================
        // LOGIN WINDOW
        // =====================================================

        var loginWindow = new LoginWindow
        {
            DataContext = loginViewModel
        };


        // =====================================================
        // LOGIN EXITOSO
        // =====================================================

        loginViewModel.LoginSucceeded += () =>
        {
            if (!_sessionService.IsAuthenticated)
            {
                return;
            }


            ShowMainWindow(
                desktop,
                loginWindow
            );
        };


        // =====================================================
        // VENTANA PRINCIPAL ACTUAL
        // =====================================================

        desktop.MainWindow =
            loginWindow;


        // =====================================================
        // MOSTRAR LOGIN
        // =====================================================

        loginWindow.Show();
    }


    // =========================================================
    // MOSTRAR MAIN WINDOW
    // =========================================================

    private void ShowMainWindow(
        IClassicDesktopStyleApplicationLifetime desktop,
        LoginWindow loginWindow)
    {
        if (_sessionService is null)
        {
            return;
        }


        // =====================================================
        // MAIN VIEWMODEL
        // =====================================================

        var mainViewModel = new MainViewModel(
            _sessionService
        );


        // =====================================================
        // MAIN WINDOW
        // =====================================================

        var mainWindow = new MainWindow
        {
            DataContext = mainViewModel
        };


        // =====================================================
        // LOGOUT
        // =====================================================

        mainViewModel.LogoutRequested += () =>
        {
            ShowLoginWindow(
                desktop
            );

            mainWindow.Close();
        };


        // =====================================================
        // CAMBIAR VENTANA PRINCIPAL
        // =====================================================

        desktop.MainWindow =
            mainWindow;


        // =====================================================
        // MOSTRAR MAIN WINDOW
        // =====================================================

        mainWindow.Show();


        // =====================================================
        // CERRAR LOGIN
        // =====================================================

        loginWindow.Close();
    }
}