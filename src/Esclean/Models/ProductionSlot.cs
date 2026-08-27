using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Esclean.Models;

public partial class ProductionSlot : ObservableObject
{
    public string Position { get; set; } = string.Empty;

    public string Line { get; set; } = string.Empty;

    public string Side { get; set; } = string.Empty;

    public string StencilCode { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string Requisitor { get; set; } = string.Empty;

    public string Responsible { get; set; } = string.Empty;

    public DateTime? StartDateTime { get; set; }


    [ObservableProperty]
    private string elapsedTime = "--:--:--";


    [ObservableProperty]
    private string statusText = "DISPONIBLE";


    [ObservableProperty]
    private IBrush statusColor =
        new SolidColorBrush(Color.Parse("#687A8C"));


    public bool IsOccupied =>
        !string.IsNullOrWhiteSpace(StencilCode);


    public string StartTime =>
        StartDateTime?.ToString("HH:mm") ?? "—";


    public void Refresh()
    {
        if (!IsOccupied || StartDateTime is null)
        {
            ElapsedTime = "--:--:--";
            StatusText = "DISPONIBLE";

            StatusColor =
                new SolidColorBrush(
                    Color.Parse("#687A8C"));

            return;
        }


        TimeSpan elapsed =
            DateTime.Now - StartDateTime.Value;


        ElapsedTime =
            $"{(int)elapsed.TotalHours:00}:" +
            $"{elapsed.Minutes:00}:" +
            $"{elapsed.Seconds:00}";


        if (elapsed.TotalHours >= 8)
        {
            StatusText = "CRÍTICO";

            StatusColor =
                new SolidColorBrush(
                    Color.Parse("#FF5C5C"));
        }
        else if (elapsed.TotalHours >= 5)
        {
            StatusText = "ALERTA";

            StatusColor =
                new SolidColorBrush(
                    Color.Parse("#F2C94C"));
        }
        else
        {
            StatusText = "NORMAL";

            StatusColor =
                new SolidColorBrush(
                    Color.Parse("#39D98A"));
        }
    }
}