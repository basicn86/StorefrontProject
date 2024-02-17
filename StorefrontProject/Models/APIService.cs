using StorefrontProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models
{
    public class APIService : IAPIService
    {
        //local host API URL
        private const string API_URL = "http://localhost:5000/";

        //get products from the database
        public async Task<IEnumerable<NetworkResources.Product>> GetProductsAsync()
        {
            HttpClient client = new HttpClient();

            client.Timeout = TimeSpan.FromSeconds(15);

            //try to get the products from the API
            HttpResponseMessage responseMessage = await client.GetAsync(API_URL + "api/GetProducts");

            //if the response is successful
            if (responseMessage.IsSuccessStatusCode)
            {
                //try to read the response as a list of products
                try
                {
                    IEnumerable<NetworkResources.Product> products = await responseMessage.Content.ReadAsAsync<IEnumerable<NetworkResources.Product>>();
                    return products;
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //return empty ienumerable to avoid the error for now
            return Enumerable.Empty<NetworkResources.Product>();
        }
    }
}
