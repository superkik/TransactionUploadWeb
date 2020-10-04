using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UploadTransaction.Library.Helper;
using UploadTransaction.Library.SystemConst;

namespace UploadTransaction.Models.InternalModels
{
    public class TransactionRawModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string TransactionId { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public string TransactionDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string FromFileType { get; set; }



        public bool IsValidModel()
        {
            var hasError = ValidateRule()?.Count() > 0;
            return hasError ? false : true;

        }

        public string[] GetErrorValidateModelMessage()
        {
            var errorList = ValidateRule();
            if (errorList != null && errorList.Any())
            {
                return errorList.ToArray();
            }
            return null;
        }

        private IEnumerable<string> ValidateRule()
        {

            var errorList = new List<string>();
            ICollection<ValidationResult> results = null;
            if (!ModelValidatorHelper.Validate(this, out results))
            {
                errorList = results.Select(o => o.ErrorMessage).ToList();
            }

            var isNumeric = double.TryParse(Amount, out _);
            if (!isNumeric)
            {
                errorList.Add("Amount value is not numberic.");
            }

            if (!CurrencyHelper.IsExist(CurrencyCode))
            {
                errorList.Add("CurrencyCode  is invalid.");
            }

            CultureInfo enUS = new CultureInfo("en-US");
            if (FromFileType == TransactionFileFromTypeConst.CSV)
            {
                if (!TransactionCSVStatusCodeConst.IsExist(Status))
                {
                    errorList.Add("Status  is invalid.");
                }

                if (!DateTime.TryParseExact(TransactionDate, "dd/MM/yyyy hh:mm:ss", enUS,
                             DateTimeStyles.None, out _))
                {
                    errorList.Add("Transaction date  is invalid format dd/MM/yyyy hh:mm:ss .");

                }

            }

            if (FromFileType == TransactionFileFromTypeConst.XML)
            {
                if (!TransactionXMLStatusCodeConst.IsExist(Status))
                {
                    errorList.Add("Status  is invalid.");
                }
                if (!DateTime.TryParseExact(TransactionDate, "s", enUS,
                             DateTimeStyles.None, out _))
                {
                    errorList.Add("Transaction date  is invalid format yyyy-MM-ddThh:mm:ss .");

                }
            }

            if (errorList.Any())
            {
                return errorList;
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        public DateTime GetTransactionDateTime()
        {
            var enUS = new CultureInfo("en-US");
            DateTime datetime = DateTime.MinValue;
            if (FromFileType == TransactionFileFromTypeConst.CSV)
            {
                datetime = DateTime.ParseExact(TransactionDate, "dd/MM/yyyy hh:mm:ss", enUS, DateTimeStyles.None);
            }
            if (FromFileType == TransactionFileFromTypeConst.XML)
            {
                datetime = DateTime.ParseExact(TransactionDate, "s", enUS, DateTimeStyles.None);
            }
            return datetime;
        }


    }
}
