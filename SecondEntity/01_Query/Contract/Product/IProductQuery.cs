using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contract.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDetails(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
