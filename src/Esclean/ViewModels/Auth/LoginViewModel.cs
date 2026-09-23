using System;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Esclean.Services.Api.Auth;
using Esclean.Services.Session;

namespace Esclean.ViewModels.Auth;

public partial class LoginViewModel : ViewModelBase
{
    private readonly IAuthApiService _authApiService;
    private readonly ISessionService _sessionService;

    // =========================================================
    // EVENTOS
    // =========================================================
    //
    // App.axaml.cs se suscribirá a este evento.
    // Se dispara únicamente cuando el usuario fue autenticado
    // correctamente y la sesión ya fue creada.
    //
    public event Action? LoginSucceeded;

    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public LoginViewModel(
        IAuthApiService authApiService,
        ISessionService sessionService)
    {
        _authApiService = authApiService;
        _sessionService = sessionService;
    }
    
    // =========================================================
    // PROPIEDADES
    // =========================================================

    [ObservableProperty]
    private string _employeeNumber = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isLoading;

    public bool HasError =>
        !string.IsNullOrWhiteSpace(ErrorMessage);

    public bool CanLogin =>
        !IsLoading;

    // =========================================================
    // LOGIN
    // =========================================================

    [RelayCommand]
    private async Task LoginAsync()
    {
        ClearError();

        // =====================================================
        // VALIDAR EMPLEADO
        // =====================================================

        if (string.IsNullOrWhiteSpace(EmployeeNumber))
        {
            ShowError("Ingresa tu número de empleado.");
            return;
        }

        if (!EmployeeNumber.All(char.IsDigit))
        {
            ShowError("El número de empleado solo puede contener números.");
            return;
        }
        
        // =====================================================
        // VALIDAR PASSWORD
        // =====================================================

        if (string.IsNullOrWhiteSpace(Password))
        {
            ShowError("Ingresa tu contraseña." );

            return;
        }

        // =====================================================
        // CONSULTAR POSTGREST
        // =====================================================

        try
        {
            IsLoading = true;

            var user = await _authApiService.LoginAsync(
                EmployeeNumber.Trim(),
                Password
            );

            // =================================================
            // LOGIN INCORRECTO
            // =================================================

            if (user is null)
            {
                ShowError("Número de empleado o contraseña incorrectos.");
                return;
            }
            
            // INICIAR SESIÓN
            _sessionService.StartSession(user);

            // =================================================
            // LIMPIAR FORMULARIO
            ClearError();
            Password = string.Empty;


            // =================================================
            // LOGIN CORRECTO
            // =================================================
            //
            // Notificamos a quien esté escuchando que el login
            // terminó correctamente.
            //
            // App.axaml.cs será responsable de:
            //
            // 1. Crear MainWindow.
            // 2. Asignar MainViewModel.
            // 3. Mostrar MainWindow.
            // 4. Cerrar LoginWindow.
            //
            LoginSucceeded?.Invoke();
        }
        catch (Exception)
        {
            ShowError("No fue posible conectar con el servicio de autenticación.");
        }
        finally
        {
            IsLoading = false;
        }
    }


    // =========================================================
    // ERROR
    // =========================================================

    private void ShowError(string message)
    {
        ErrorMessage = message;

        OnPropertyChanged(
            nameof(HasError)
        );
    }


    private void ClearError()
    {
        ErrorMessage = string.Empty;

        OnPropertyChanged(
            nameof(HasError)
        );
    }


    // =========================================================
    // CAMBIO DE ESTADO
    // =========================================================

    partial void OnIsLoadingChanged(bool value)
    {
        OnPropertyChanged(
            nameof(CanLogin)
        );
    }
}