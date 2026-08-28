using Avalonia.Controls;
using Avalonia.Interactivity;
using Esclean.ViewModels.Stencils;

namespace Esclean.Views.Stencils;

public partial class StencilDeliveryView : UserControl
{
    public StencilDeliveryView()
    {
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private async void OnLoaded(
        object? sender,
        RoutedEventArgs e)
    {
        if (DataContext is StencilDeliveryViewModel viewModel)
        {
            await viewModel.InitializeAsync();
        }
    }
}