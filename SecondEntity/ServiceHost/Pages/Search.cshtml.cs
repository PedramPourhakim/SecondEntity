using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Query.Contract.Product;
using _01_Query.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value;
        public List<ProductQueryModel> Products;
        private readonly IProductQuery productQuery;

        public SearchModel(IProductQuery productQuery)
        {
            this.productQuery = productQuery;
        }

        public void OnGet(string value)
        {
            Value = value;
            Products = productQuery
                .Search(value);
        }
    }
}
