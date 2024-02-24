using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkResources
{
    //This is usually sent from the client to the server to place an order
    public record OrderRequest
    {
        //dictionary of int to ints
        //the key is the product ID
        //the value is the quantity
        //The reason why we request the IDs and not the products themselves is because the client cannot be trusted to send the correct product information.
        public Dictionary<int, int> OrderItems { get; init; } = new Dictionary<int, int>();
        //total price of the order as a decimal
        //Important Note: this is to verify that the client and server agree on the total price
        public decimal TotalPrice { get; init; }
    }
}
