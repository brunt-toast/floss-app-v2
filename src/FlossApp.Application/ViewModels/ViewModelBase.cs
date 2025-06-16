using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FlossApp.Application.ViewModels;

public abstract class ViewModelBase : ObservableObject
{
    protected IServiceProvider Services { get; }

    protected ViewModelBase(IServiceProvider services)
    {
        Services = services;
    }
}
