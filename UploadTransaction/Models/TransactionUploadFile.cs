using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UploadTransaction.Library.DataAttibute;
using UploadTransaction.Models.InternalModels;

namespace UploadTransaction.Models
{
    public class TransactionUploadFile
    {

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSizeMB(1)]
        [AllowedExtensions(new string[] { ".csv", ".xml" })]
        public IFormFile File { get; set; }

        public List<TransactionRawModel> UploadTransactions { get; set; }
    }
}
