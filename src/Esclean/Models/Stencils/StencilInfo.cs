namespace Esclean.Models.Stencils;

public class StencilInfo
{
    public string SteelNo { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string Side { get; set; } = string.Empty;

    public string CurrentLocation { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public bool Active { get; set; }

    public bool Usable { get; set; }
}