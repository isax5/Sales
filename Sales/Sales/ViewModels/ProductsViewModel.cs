using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using Sales.Common.Models;
using Sales.Services;

namespace Sales.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        /// <summary>
        /// Servicio para solicitar productos
        /// </summary>
        private ApiService apiService;

        /// <summary>
        /// Lista de productos
        /// </summary>
        private ObservableRangeCollection<Product> products;

        /// <summary>
        /// Acceso a lista de productos
        /// </summary>
        public ObservableRangeCollection<Product> Products
        {
            get { return products; }
            set { SetProperty(ref products, value, "Products"); }
        }

        public ProductsViewModel()
        {
            apiService = new ApiService();
            LoadProductos();
        }

        private async void LoadProductos()
        {
            var response = await apiService.GetList<Product>("Products");

            if (!response.IsSeccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Cerrar");
                return;
            }

            var list = (List<Product>)response.Result;
            Products = new ObservableRangeCollection<Product>(list);
        }
    }
}
