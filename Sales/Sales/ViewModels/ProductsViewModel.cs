using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmHelpers;
using Sales.Common.Models;
using Sales.Helpers;
using Sales.Services;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        /// <summary>
        /// Servicio para solicitar productos
        /// </summary>
        private ApiService apiService;

        #region Bindings Attributes
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

        
        /// <summary>
        /// Mostrar y ocultar cargando para lista
        /// </summary>
        private bool isRefreshing;

        /// <summary>
        /// Acceso a cargando para lista
        /// </summary>
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value, "IsRefreshing"); }
        }
        #endregion

        public ProductsViewModel()
        {
            apiService = new ApiService();
            LoadProductos();
        }

        private async void LoadProductos()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSeccess)
            {
                IsRefreshing = false;
                if (Application.Current.MainPage != null)
                    await Application.Current.MainPage.DisplayAlert(Languajes.Error , connection.Message, Languajes.Accept);
                return;
            }


            var response = await apiService.GetList<Product>("Products");

            if (!response.IsSeccess)
            {
                IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert(Languajes.Error, response.Message, Languajes.Accept);
                return;
            }

            var list = (List<Product>)response.Result;
            Products = new ObservableRangeCollection<Product>(list);

            IsRefreshing = false;
        }


        #region Commands
        /// <summary>
        /// Comando de recarga para lista de productos
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProductos);
            }
        }
        #endregion
    }
}
