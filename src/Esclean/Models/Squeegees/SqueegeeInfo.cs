using System;

namespace Esclean.Models.Squeegees;

public class SqueegeeInfo
{
    public short IdHolderType { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Size { get; set; }

    public int TotalQuantity { get; set; }

    public bool Active { get; set; }

    public string? Comments { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}