using StorefrontProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StorefrontProject.Models
{
    public class ApiClient : IApiClient
    {
        //local host API URL
        private const string API_URL = "http://localhost:5000/";

        //get products from the database
        public async Task<IEnumerable<NetworkResources.Product>> GetProductsAsync()
        {
            HttpClient client = new HttpClient();

            client.Timeout = TimeSpan.FromSeconds(15);

            //try to get the products from the API
            HttpResponseMessage responseMessage = await client.GetAsync(API_URL + "api/products");

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

        //placeholder for placing an order
        public async Task PlaceOrderAsync(NetworkResources.Order order)
        {
            //new HTTP client
            HttpClient client = new HttpClient();

            //client timeout
            client.Timeout = TimeSpan.FromSeconds(15);

            //http response
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(API_URL + "api/orders", order);

            //if the response is not successful
            if (!responseMessage.IsSuccessStatusCode)
            {
                string responseMsg = await responseMessage.Content.ReadAsStringAsync();

                throw new Exception("Server: " + responseMsg);
            }

            //return nothing
        }

        //get orders from the server
        public async Task<IEnumerable<NetworkResources.Order>> GetOrdersAsync()
        {
            //new HTTP client
            HttpClient client = new HttpClient();

            //client timeout
            client.Timeout = TimeSpan.FromSeconds(15);

            //http response
            HttpResponseMessage responseMessage = await client.GetAsync(API_URL + "api/orders");

            //if the response is successful
            if (responseMessage.IsSuccessStatusCode)
            {
                //try to read the response as a list of orders
                try
                {
                    IEnumerable<NetworkResources.Order> orders = await responseMessage.Content.ReadAsAsync<IEnumerable<NetworkResources.Order>>();
                    return orders;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //return empty ienumerable to avoid the error for now
            return Enumerable.Empty<NetworkResources.Order>();
        }

        //implement the function to remove the order
        public async Task RemoveOrderAsync(Guid orderId)
        {
            //new HTTP client
            HttpClient client = new HttpClient();

            //client timeout
            client.Timeout = TimeSpan.FromSeconds(15);

            //http response
            HttpResponseMessage responseMessage = await client.DeleteAsync(API_URL + "api/orders?id=" + orderId);

            //if the response is successful
            if (responseMessage.IsSuccessStatusCode)
            {
                return;
            }

            //throw an exception if the response is not successful
            throw new Exception("Server:" + await responseMessage.Content.ReadAsStringAsync());
        }

        //implement the function to update the order
        public async Task UpdateOrderAsync(NetworkResources.Order order)
        {
            //new HTTP client
            HttpClient client = new HttpClient();

            //client timeout
            client.Timeout = TimeSpan.FromSeconds(15);

            //http response
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(API_URL + "api/orders", order);

            //if the response is successful
            if (responseMessage.IsSuccessStatusCode)
            {
                return;
            }

            //throw an exception if the response is not successful, and display the response body
            throw new Exception("Server: " + await responseMessage.Content.ReadAsStringAsync());
        }
    }
}
