using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory :EntityBase
    {
        public string Name { get;private set; }
        public string Description { get;private set; }
        public string Picture { get;private set; }
        public string PictureAlt { get;private set; }
        public string PictureTitle { get;private set; }
        public string Keywords { get;private set; }
        public string  MetaDescription { get;private set; }
        public string Slug { get; private set; }
        public List<Product> Products { get; private set; }
        public ProductCategory()
        {
            Products = new List<Product>();
        }
        public ProductCategory(string Name,string Description
            ,string Picture,string PictureAlt,string PictureTitle,
            string Keywords,string MetaDescription,string Slug)
        {
            this.Name = Name;
            this.Description = Description;
            this.Picture = Picture;
            this.PictureAlt = PictureAlt;
            this.PictureTitle = PictureTitle;
            this.Keywords = Keywords;
            this.MetaDescription = MetaDescription;
            this.Slug = Slug;
        }
        public void Edit(string Name,string Description,
            string Picture,string PictureAlt,string PictureTitle
            ,string Keywords,string MetaDescription,string Slug)
        {
            this.Name = Name;
            this.Description = Description;
            this.Picture = Picture;
            this.PictureAlt = PictureAlt;
            this.PictureTitle = PictureTitle;
            this.Keywords = Keywords;
            this.MetaDescription = MetaDescription;
            this.Slug = Slug;
        }
    }
}
