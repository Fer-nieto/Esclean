using System;

namespace Esclean.Models.Stencils;

public class StencilMovement
{
    public string IdMovement { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string SteelNo { get; set; } = string.Empty;

    public string MovementType { get; set; } = string.Empty;

    public string Line { get; set; } = string.Empty;

    public string Side { get; set; } = string.Empty;

    public string Requisitor { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Comments { get; set; } = string.Empty;

    public string DateText => Date.ToString("dd/MM/yyyy HH:mm:ss");
}