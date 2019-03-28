using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.ViewModels
{
    /// <summary>
    /// ViewModel principal para exponer los elementos al iniciar la app
    /// </summary>
    public class MainViewModel
    {
        public ProductsViewModel Products { get; set; }

        public MainViewModel()
        {
            Products = new ProductsViewModel();
        }
    }
}
