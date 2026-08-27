using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Esclean.ViewModels;

namespace Esclean;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;


        var viewModelType =
            data.GetType();


        var viewName =
            viewModelType.FullName!
                .Replace(
                    ".ViewModels.",
                    ".Views.",
                    StringComparison.Ordinal)
                .Replace(
                    "ViewModel",
                    "View",
                    StringComparison.Ordinal);


        var viewType =
            Type.GetType(viewName);


        if (viewType != null)
        {
            return (Control)
                Activator.CreateInstance(viewType)!;
        }


        return new TextBlock
        {
            Text =
                $"Vista no encontrada:\n{viewName}"
        };
    }


    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}