using Avalonia.Controls;
using Avalonia.Interactivity;
using Esclean.ViewModels.Stencils;

namespace Esclean.Views.Stencils;

public partial class StencilDamageReportView : UserControl
{
    public StencilDamageReportView()
    {
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private async void OnLoaded(
        object? sender,
        RoutedEventArgs e)
    {
        if (DataContext is StencilDamageReportViewModel viewModel)
        {
            await viewModel.InitializeAsync();
        }
    }
}