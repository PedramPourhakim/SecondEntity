using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contract.Inventory
{
    public class IsInStock
    {
        public long ProductId { get; set; }
        public int Count { get; set; }
    }
}
