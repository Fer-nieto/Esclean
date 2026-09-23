using System;
namespace Esclean.Models.Trays;

public class TrayInfo
{
    public string IdTray { get; set; } = string.Empty;

    public string? Name { get; set; }

    public string? CutModel { get; set; }

    public string? UseModelPanel { get; set; }

    public string? Supplier { get; set; }

    public int Spaces { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}