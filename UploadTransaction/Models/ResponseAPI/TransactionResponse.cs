using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UploadTransaction.Library.SystemConst;
using UploadTransaction.Models.DB;

namespace UploadTransaction.Models.ResponseAPI
{
    public class TransactionResponse
    {
        public string Id { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }


        public TransactionResponse(TransactionUpload transaction)
        {
            var enUS = new CultureInfo("en-US");
            this.Id = transaction.TransactionId;
            this.Payment = $"{transaction.Amount.ToString("N2")} {transaction.Currency}";
            this.Status = transaction.Status;
            if (transaction.FromFile == TransactionFileFromTypeConst.CSV)
            {
                if (TransactionCSVStatusCodeConst.Approved.ToUpper() == this.Status)
                {
                    this.Status = "A";
                }
                else if (TransactionCSVStatusCodeConst.Failed.ToUpper() == this.Status)
                {
                    this.Status = "R";
                }
                else if (TransactionCSVStatusCodeConst.Finished.ToUpper() == this.Status)
                {
                    this.Status = "D";
                }
            }
            else if (transaction.FromFile == TransactionFileFromTypeConst.XML)
            {
                if (TransactionXMLStatusCodeConst.Approved.ToUpper() == this.Status)
                {
                    this.Status = "A";
                }
                else if (TransactionXMLStatusCodeConst.Rejected.ToUpper() == this.Status)
                {
                    this.Status = "R";
                }
                else if (TransactionXMLStatusCodeConst.Done.ToUpper() == this.Status)
                {
                    this.Status = "D";
                }
            }
            //this.TransactionDate = transaction.TransactionDate.HasValue ? transaction.TransactionDate.Value.ToString("dd/MM/yyyy hh:mm:ss", enUS) : "";
        }
    }
}
