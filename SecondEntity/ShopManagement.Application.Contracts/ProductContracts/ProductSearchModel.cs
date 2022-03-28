using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductContracts
{
    public class ProductSearchModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long CategoryId { get; set; }
    }
}
