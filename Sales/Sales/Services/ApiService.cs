using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Sales.Common.Models;
using Sales.Helpers;

namespace Sales.Services
{
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSeccess = false,
                    Message = Languajes.NoInternet
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(Constants.URLTesting);
            if (!isReachable)
            {
                return new Response
                {
                    IsSeccess = false,
                    Message = Languajes.NoInternet
                };
            }

            return new Response { IsSeccess = true };
        }
        

        /// <summary>
        /// Solicitar consulta de lista de elementos JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlBase"></param>
        /// <param name="prefix"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public async Task<Response> GetList<T>(string controller, 
            string prefix = Constants.ApiPrefix, 
            string urlBase = Constants.URLBaseAPI)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response { IsSeccess = false, Message = answer };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);

                return new Response { IsSeccess = true, Result = list };
            }
            catch (Exception ex)
            {
                return new Response { IsSeccess = false, Message = ex.Message };
            }
        }
    }
}
