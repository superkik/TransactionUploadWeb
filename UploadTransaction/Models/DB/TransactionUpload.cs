using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UploadTransaction.Models.DB
{
    public partial class TransactionUpload
    {
        [Key]
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Status { get; set; }
        public string FromFile { get; set; }

    }
}
