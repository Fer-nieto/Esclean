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

    // =========================================================
    // MODO DE ARRANQUE
    // =========================================================
    //
    // true  = entra directamente a MainWindow
    // false = utiliza LoginWindow normalmente
    //
    // Para trabajar en la UI:
    //
    //     private const bool SkipLogin = true;
    //
    // Para regresar al login:
    //
    //     private const bool SkipLogin = false;
    //
    // =========================================================

    private const bool SkipLogin = true;


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
            // MODO DE ARRANQUE
            // =====================================================

            if (SkipLogin)
            {
                // -------------------------------------------------
                // MODO DESARROLLO
                // -------------------------------------------------
                //
                // No se muestra LoginWindow.
                // Se abre directamente MainWindow.
                //
                // -------------------------------------------------

                ShowMainWindowDirect(
                    desktop
                );
            }
            else
            {
                // -------------------------------------------------
                // MODO NORMAL
                // -------------------------------------------------
                //
                // Se utiliza el flujo real de Login.
                //
                // -------------------------------------------------

                ShowLoginWindow(
                    desktop
                );
            }
        }

        base.OnFrameworkInitializationCompleted();
    }


    // =========================================================
    // MOSTRAR MAIN WINDOW DIRECTAMENTE
    // =========================================================
    //
    // Este método se utiliza solamente cuando:
    //
    //     SkipLogin = true
    //
    // =========================================================

    private void ShowMainWindowDirect(
        IClassicDesktopStyleApplicationLifetime desktop)
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
        //
        // Aunque estamos saltando el login al iniciar,
        // conservamos el comportamiento de Logout.
        //
        // =====================================================

        mainViewModel.LogoutRequested += () =>
        {
            ShowLoginWindow(
                desktop
            );

            mainWindow.Close();
        };


        // =====================================================
        // VENTANA PRINCIPAL
        // =====================================================

        desktop.MainWindow =
            mainWindow;


        // =====================================================
        // MOSTRAR MAIN WINDOW
        // =====================================================

        mainWindow.Show();
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
    // MOSTRAR MAIN WINDOW DESPUÉS DEL LOGIN
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