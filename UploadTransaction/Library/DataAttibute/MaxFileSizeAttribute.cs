using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UploadTransaction.Library.DataAttibute
{
    public class MaxFileSizeMBAttribute : ValidationAttribute
    {
        private readonly int _maxFileSizeBtye;

        private readonly int _maxFileSize;

        public MaxFileSizeMBAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
            _maxFileSizeBtye = maxFileSize * 1024 * 1024;

        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSizeBtye)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is { _maxFileSize} bytes.";
        }
    }
}
