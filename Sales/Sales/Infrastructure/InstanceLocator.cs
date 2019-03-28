using Sales.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructure
{
    /// <summary>
    /// Expone principal ViewModel al comenzar la app
    /// </summary>
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
