using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Esclean.Models.Stencils;

namespace Esclean.Services.Api.Stencils;

public class MockStencilApiService : IStencilApiService
{
    // =========================================================
    // MOVIMIENTOS
    // =========================================================

    private readonly List<StencilMovement> _movements =
    [
        new StencilMovement
        {
            IdMovement = "MOV-ST-001",
            Date = DateTime.Now.AddMinutes(-8),
            SteelNo = "ST-1001",
            MovementType = "OUT",
            Line = "AG01",
            Side = "TOP",
            Requisitor = "100123",
            Status = "EN PISO"
        },

        new StencilMovement
        {
            IdMovement = "MOV-ST-002",
            Date = DateTime.Now.AddMinutes(-17),
            SteelNo = "ST-1002",
            MovementType = "IN",
            Line = "AG03",
            Side = "BOT",
            Requisitor = "100245",
            Status = "CLEANING"
        },

        new StencilMovement
        {
            IdMovement = "MOV-ST-003",
            Date = DateTime.Now.AddMinutes(-31),
            SteelNo = "ST-1003",
            MovementType = "OUT",
            Line = "AG05",
            Side = "TOP",
            Requisitor = "100310",
            Status = "EN PISO"
        }
    ];


    // =========================================================
    // REPORTES DE DAÑO
    // =========================================================

    private readonly List<StencilDamageReport> _damageReports =
    [
        new StencilDamageReport
        {
            IdReport = "DMG-ST-001",
            Date = DateTime.Now.AddHours(-1),
            SteelNo = "ST-1008",
            Model = "ORIOLE",
            Side = "TOP",
            Line = "AG04",
            Reporter = "100120",
            DamageType = "DAÑO EN MALLA",
            Description = "Daño visible en área inferior.",
            Status = "PENDIENTE"
        },

        new StencilDamageReport
        {
            IdReport = "DMG-ST-002",
            Date = DateTime.Now.AddHours(-3),
            SteelNo = "ST-1012",
            Model = "JUPITER",
            Side = "BOT",
            Line = "AG02",
            Reporter = "100225",
            DamageType = "TENSIÓN",
            Description = "Tensión fuera de especificación.",
            Status = "EN REVISIÓN"
        }
    ];


    // =========================================================
    // BUSCAR STENCIL
    // =========================================================

    public Task<StencilInfo?> GetStencilAsync(
        string steelNo)
    {
        if (string.IsNullOrWhiteSpace(steelNo))
        {
            return Task.FromResult<StencilInfo?>(null);
        }

        var stencil =
            new StencilInfo
            {
                SteelNo = steelNo.Trim(),
                Model = "ORIOLE",
                Side = "TOP",
                CurrentLocation = "CLEANING ROOM",
                Status = "DISPONIBLE",
                Active = true,
                Usable = true
            };

        return Task.FromResult<StencilInfo?>(
            stencil);
    }


    // =========================================================
    // MOVIMIENTOS
    // =========================================================

    public Task<List<StencilMovement>> GetLastMovementsAsync(
        int limit = 20)
    {
        var result =
            _movements
                .OrderByDescending(x => x.Date)
                .Take(limit)
                .ToList();

        return Task.FromResult(result);
    }


    public Task<bool> CreateMovementAsync(
        StencilMovement movement)
    {
        movement.IdMovement =
            $"MOV-ST-{_movements.Count + 1:000}";

        movement.Date =
            DateTime.Now;

        movement.Status =
            movement.MovementType == "OUT"
                ? "EN PISO"
                : "CLEANING";

        if (string.IsNullOrWhiteSpace(
                movement.Side))
        {
            movement.Side = "TOP";
        }

        _movements.Insert(
            0,
            movement);

        return Task.FromResult(true);
    }


    // =========================================================
    // REPORTES DE DAÑO
    // =========================================================

    public Task<List<StencilDamageReport>>
        GetLastDamageReportsAsync(
            int limit = 20)
    {
        var result =
            _damageReports
                .OrderByDescending(x => x.Date)
                .Take(limit)
                .ToList();

        return Task.FromResult(result);
    }


    public Task<bool> CreateDamageReportAsync(
        StencilDamageReport report)
    {
        report.IdReport =
            $"DMG-ST-{_damageReports.Count + 1:000}";

        report.Date =
            DateTime.Now;

        report.Status =
            "PENDIENTE";

        _damageReports.Insert(
            0,
            report);

        return Task.FromResult(true);
    }
}