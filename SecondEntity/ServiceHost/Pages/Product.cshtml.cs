using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Query.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery productQuery;
        public ProductModel(IProductQuery productQuery)
        {
            this.productQuery = productQuery;
        }
        public void OnGet(string id)
        {
            Product = productQuery.GetDetails(id);
        }
    }
}
