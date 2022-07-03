﻿using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopManagement.Application.Contracts.ProductPictureContracts
{
    public class CreateProductPicture
    {
        [Range(1,100000,ErrorMessage =ValidationMessages.IsRequired)]
        public long ProductId { get; set; }
        [MaxFileSize( 1* 1024 * 1024 , ErrorMessage =ValidationMessages.MaxFileSize)]
        public IFormFile Picture { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
