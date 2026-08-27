using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Esclean.Models;

namespace Esclean.ViewModels.Dashboard;

public partial class DashboardViewModel : ViewModelBase
{
    public ObservableCollection<ProductionSlot> ProductionSlots
    {
        get;
    }


    [ObservableProperty]
    private int totalFloor;


    [ObservableProperty]
    private int totalNormal;


    [ObservableProperty]
    private int totalWarning;


    [ObservableProperty]
    private int totalCritical;


    private readonly DispatcherTimer _timer;


    public DashboardViewModel()
    {
        ProductionSlots =
            new ObservableCollection<ProductionSlot>();


        LoadTestData();


        RefreshDashboard();


        _timer =
            new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };


        _timer.Tick += (_, _) =>
        {
            RefreshDashboard();
        };


        _timer.Start();
    }


    private void RefreshDashboard()
    {
        foreach (var slot in ProductionSlots)
        {
            slot.Refresh();
        }


        TotalFloor =
            ProductionSlots.Count(
                x => x.IsOccupied);


        TotalNormal =
            ProductionSlots.Count(
                x => x.StatusText == "NORMAL");


        TotalWarning =
            ProductionSlots.Count(
                x => x.StatusText == "ALERTA");


        TotalCritical =
            ProductionSlots.Count(
                x => x.StatusText == "CRÍTICO");
    }


    private void LoadTestData()
    {
        DateTime now =
            DateTime.Now;


        Add(
            "SMTAG01A",
            "AG01",
            "BOT",
            "ST-12045",
            "JUPITER",
            now.AddHours(-2.4),
            "102345",
            "100912");


        Add(
            "SMTAG01B",
            "AG01",
            "TOP",
            "ST-12046",
            "JUPITER",
            now.AddHours(-5.9),
            "105821",
            "103452");


        Add(
            "SMTAG02A",
            "AG02",
            "BOT",
            "ST-55321",
            "DOVE",
            now.AddHours(-9.3),
            "101224",
            "100754");


        AddEmpty(
            "SMTAG02B",
            "AG02",
            "TOP");


        Add(
            "SMTAG03A",
            "AG03",
            "BOT",
            "ST-87914",
            "ORIOLE",
            now.AddHours(-1.7),
            "108512",
            "104411");


        Add(
            "SMTAG03B",
            "AG03",
            "TOP",
            "ST-87915",
            "ORIOLE",
            now.AddHours(-6.4),
            "103452",
            "102001");


        Add(
            "SMTAG04A",
            "AG04",
            "BOT",
            "ST-44012",
            "SPARROW",
            now.AddHours(-10.7),
            "100912",
            "105821");


        Add(
            "SMTAG04B",
            "AG04",
            "TOP",
            "ST-44013",
            "SPARROW",
            now.AddHours(-3.2),
            "104411",
            "101224");


        AddEmpty(
            "SMTAG05A",
            "AG05",
            "BOT");


        Add(
            "SMTAG05B",
            "AG05",
            "TOP",
            "ST-33155",
            "TPM",
            now.AddHours(-0.9),
            "102001",
            "106824");


        Add(
            "SMTAG06A",
            "AG06",
            "BOT",
            "ST-66521",
            "UPDB",
            now.AddHours(-5.3),
            "104965",
            "103338");


        Add(
            "SMTAG06B",
            "AG06",
            "TOP",
            "ST-66522",
            "UPDB",
            now.AddHours(-3.6),
            "101475",
            "108512");


        Add(
            "SMTAG07A",
            "AG07",
            "BOT",
            "ST-77124",
            "EVAS",
            now.AddHours(-11.1),
            "100754",
            "104965");


        Add(
            "SMTAG07B",
            "AG07",
            "TOP",
            "ST-77125",
            "EVAS",
            now.AddHours(-6.6),
            "106824",
            "101475");


        Add(
            "SMTAG08A",
            "AG08",
            "BOT",
            "ST-99231",
            "UC-MODULE",
            now.AddHours(-2.2),
            "103338",
            "102345");


        AddEmpty(
            "SMTAG08B",
            "AG08",
            "TOP");
    }


    private void Add(
        string position,
        string line,
        string side,
        string stencil,
        string model,
        DateTime start,
        string requisitor,
        string responsible)
    {
        ProductionSlots.Add(
            new ProductionSlot
            {
                Position = position,
                Line = line,
                Side = side,
                StencilCode = stencil,
                Model = model,
                StartDateTime = start,
                Requisitor = requisitor,
                Responsible = responsible
            });
    }


    private void AddEmpty(
        string position,
        string line,
        string side)
    {
        ProductionSlots.Add(
            new ProductionSlot
            {
                Position = position,
                Line = line,
                Side = side
            });
    }
}