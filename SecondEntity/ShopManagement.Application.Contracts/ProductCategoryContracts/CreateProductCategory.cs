using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductCategoryContracts
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Name { get; set; }
        public string Description { get;  set; }
      
        [FileExtensionValidation(new string[] {".jpeg",".jpg",".jfif",".png" },ErrorMessage =ValidationMessages.InvalidFileFormat)]
        [maxFileSize(3*1024*1024,ErrorMessage =ValidationMessages.MaxFileSize)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get;  set; }
    }
}
