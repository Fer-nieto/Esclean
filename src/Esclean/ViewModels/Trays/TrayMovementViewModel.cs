using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32.SafeHandles;

using System;

namespace Esclean.ViewModels.Trays;

public partial class TrayMovementViewModel: ViewModelBase
{
    // private readonly ITrayApiService _apiService;
    [ObservableProperty]
    private string _id_tray = string.Empty;
    
    
    
    
    [ObservableProperty]
    private string _movementType = "OUT";
}