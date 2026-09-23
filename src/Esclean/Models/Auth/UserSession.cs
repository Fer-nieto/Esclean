using System;
using System.Text.Json.Serialization;

namespace Esclean.Models.Auth;

public class UserSession
{
    // =========================================================
    // IDENTIFICADOR
    // =========================================================

    [JsonPropertyName("id_user")]
    public Guid IdUser { get; set; }


    // =========================================================
    // EMPLEADO
    // =========================================================

    [JsonPropertyName("employee_number")]
    public string EmployeeNumber { get; set; } = string.Empty;


    // =========================================================
    // NOMBRE
    // =========================================================

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;


    // =========================================================
    // ROL
    // =========================================================

    [JsonPropertyName("id_role")]
    public short IdRole { get; set; }


    [JsonPropertyName("role_code")]
    public string RoleCode { get; set; } = string.Empty;


    [JsonPropertyName("role_name")]
    public string RoleName { get; set; } = string.Empty;


    // =========================================================
    // ESTADO DE AUTENTICACIÓN
    // =========================================================

    [JsonIgnore]
    public bool IsAuthenticated =>
        IdUser != Guid.Empty &&
        !string.IsNullOrWhiteSpace(EmployeeNumber);
}