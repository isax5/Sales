using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sales.Common.Models;

namespace Sales.Services
{
    public class ApiService
    {
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
