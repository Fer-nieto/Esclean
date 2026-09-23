using System;
namespace Esclean.Models.Trays;

public class TrayMovement
{
    public long IdMovement { get; set; }

    public string IdTray { get; set; } = string.Empty;

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public int Quantity { get; set; }

    public string? Comments { get; set; }

    public Guid IdUser { get; set; }

    public DateTime CreatedAt { get; set; }
}
