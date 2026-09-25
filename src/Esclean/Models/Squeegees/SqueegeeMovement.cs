using System;

namespace Esclean.Models.Squeegees;

public class SqueegeeMovement
{
    public Guid IdMovement { get; set; }

    public short IdBladeType { get; set; }

    public string MovementType { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public Guid? RequestedBy { get; set; }

    public Guid HandledBy { get; set; }

    public string? Comments { get; set; }

    public DateTime MovementAt { get; set; }

    // Datos descriptivos para mostrar en la UI
    public string? BladeName { get; set; }

    public string? RequestedByName { get; set; }

    public string? HandledByName { get; set; }
}