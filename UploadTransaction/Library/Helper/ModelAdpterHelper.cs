using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UploadTransaction.Library.Mapper;
using UploadTransaction.Library.SystemConst;
using UploadTransaction.Models.InternalModels;

namespace UploadTransaction.Library.Helper
{
    public static class ModelAdpterHelper
    {
        public static IEnumerable<TransactionRawModel> FetchDataToTransactionRawModel(IFormFile formFile)
        {
            IEnumerable<TransactionRawModel> list = null;
            if (MimeTypeHelper.IsMatchMimeType(ExtensionFileConst.CSV, formFile))
            {
                List<CSVTransactionItem> csvList = null;
                using (var reader = new StreamReader(formFile.OpenReadStream()))
                {
                    using (var csvReader = new CsvReader(reader, CultureInfo.GetCultureInfo("en-US")))
                    {
                        csvReader.Configuration.HasHeaderRecord = false;
                        csvReader.Configuration.RegisterClassMap<TransactionMap>();
                        csvList = csvReader.GetRecords<CSVTransactionItem>().ToList();

                        if (csvList != null && csvList.Any())
                        {
                            list = csvList.Select(item => new TransactionRawModel
                            {
                                TransactionId = item.TransactionId,
                                Status = item.Status,
                                TransactionDate = item.TransactionDate,
                                Amount = item.Amount,
                                CurrencyCode = item.CurrencyCode,
                                FromFileType = TransactionFileFromTypeConst.CSV
                            });
                        }

                    }
                }
            }
            else if (MimeTypeHelper.IsMatchMimeType(ExtensionFileConst.XML, formFile))
            {
                var serializer = new XmlSerializer(typeof(XMLTransactions));
                var myDocument = (XMLTransactions)serializer.Deserialize(formFile.OpenReadStream());
                if (myDocument != null && myDocument.Transaction != null && myDocument.Transaction.Any())
                {
                    list = myDocument.Transaction.Select(item => new TransactionRawModel
                    {
                        TransactionId = item.Id,
                        Status = item.Status,
                        TransactionDate = item.TransactionDate,
                        Amount = item.PaymentDetails.Amount,
                        CurrencyCode = item.PaymentDetails.CurrencyCode,
                        FromFileType = TransactionFileFromTypeConst.XML
                    });
                }
            }

            return list ?? Enumerable.Empty<TransactionRawModel>();
        }
    }
}
