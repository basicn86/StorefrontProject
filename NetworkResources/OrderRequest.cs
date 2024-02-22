using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkResources
{
    public record OrderRequest
    {
        //dictionary of int to ints
        public Dictionary<int, int> OrderItems { get; init; } = new Dictionary<int, int>();
        //total price of the order as a decimal
        public decimal TotalPrice { get; init; }
    }
}
