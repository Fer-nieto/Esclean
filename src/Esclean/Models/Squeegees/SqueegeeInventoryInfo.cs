using System;

namespace Esclean.Models.Squeegees;

public class SqueegeeInventoryInfo
{
    public short IdBladeType { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Size { get; set; }

    public string? Specification { get; set; }

    public int CurrentQuantity { get; set; }

    public int MinimumQuantity { get; set; }

    public bool Active { get; set; }

    public string? Comments { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Útil para la interfaz
    public bool IsLowStock =>
        CurrentQuantity <= MinimumQuantity;
}