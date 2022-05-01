using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class maxFileSizeAttribute :ValidationAttribute , IClientModelValidator
    {
        private readonly int MaxFileSize;
        public maxFileSizeAttribute(int MaxFileSize)
        {
            this.MaxFileSize = MaxFileSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            if (file.Length > MaxFileSize)
                return false;
            return true;
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val",ErrorMessage);
        }
    }
}
