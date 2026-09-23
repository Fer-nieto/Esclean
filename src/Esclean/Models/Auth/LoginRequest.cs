using System.Text.Json.Serialization;

namespace Esclean.Models.Auth;

public class LoginRequest
{
    [JsonPropertyName("p_employee_number")]
    public string EmployeeNumber { get; set; } = string.Empty;

    [JsonPropertyName("p_password")]
    public string Password { get; set; } = string.Empty;
}