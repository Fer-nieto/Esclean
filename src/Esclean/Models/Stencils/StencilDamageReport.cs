using System;

namespace Esclean.Models.Stencils;

public class StencilDamageReport
{
    public string IdReport { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string SteelNo { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string Side { get; set; } = string.Empty;

    public string Line { get; set; } = string.Empty;

    public string Reporter { get; set; } = string.Empty;

    public string DamageType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string DateText =>
        Date.ToString("dd/MM/yyyy HH:mm:ss");
}
