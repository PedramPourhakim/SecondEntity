using _0_Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product:EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public long CategoryId { get; private set; }
        public string Slug { get; private set; }
        public string MetaDescription { get; private set; }
        public string Keywords { get; private set; }
        public ProductCategory Category { get; private set; }
        public List<ProductPicture> ProductPictures { get; private set; }

        public Product(string Name, string Code, 
             string ShortDescription, 
            string Description, string Picture,
            string PictureAlt, string PictureTitle, 
            long CategoryId, string Slug, 
            string MetaDescription, string Keywords)
        {
            this.Name = Name;
            this.Code = Code;
            this.ShortDescription = ShortDescription;
            this.Description = Description;
            this.Picture = Picture;
            this.PictureAlt = PictureAlt;
            this.PictureTitle = PictureTitle;
            this.CategoryId = CategoryId;
            this.Slug = Slug;
            this.MetaDescription = MetaDescription;
            this.Keywords = Keywords;
        }
        public void Edit(string Name, string Code,
           string ShortDescription,
          string Description, string Picture,
          string PictureAlt, string PictureTitle,
          long CategoryId, string Slug,
          string MetaDescription, string Keywords)
        {
            this.Name = Name;
            this.Code = Code;
            this.ShortDescription = ShortDescription;
            this.Description = Description;
            if (!string.IsNullOrWhiteSpace(Picture))
                this.Picture = Picture;
            this.PictureAlt = PictureAlt;
            this.PictureTitle = PictureTitle;
            this.CategoryId = CategoryId;
            this.Slug = Slug;
            this.MetaDescription = MetaDescription;
            this.Keywords = Keywords;
        }
      
    }
}
