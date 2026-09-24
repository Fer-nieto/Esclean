using System;
namespace Esclean.Models.Trays;

public class TrayInventoryInfo
{
    public string IdTray { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public DateTime UpdatedAt { get; set; }
}