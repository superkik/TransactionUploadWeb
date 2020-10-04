using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CsvHelper;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UploadTransaction.Library.DataAttibute;
using UploadTransaction.Library.Helper;
using UploadTransaction.Library.Mapper;
using UploadTransaction.Library.SystemConst;
using UploadTransaction.Models;
using UploadTransaction.Models.DB;
using UploadTransaction.Models.InternalModels;

namespace UploadTransaction.Controllers
{
    public class HomeController : Controller
    {

        private InvoiceContext _InvoiceDB;

        public HomeController(InvoiceContext dbContext)
        {
            _InvoiceDB = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(TransactionUploadFile uploadFIle)
        {

            if (ModelState.IsValid)
            {
                var transactionModel = ModelAdpterHelper.FetchDataToTransactionRawModel(uploadFIle.File);
                if (transactionModel != null && transactionModel.Any())
                {
                    var hasRecordInvalid = transactionModel.Where(item => !item.IsValidModel()).Any();
                    if (hasRecordInvalid)
                    {
                        uploadFIle.UploadTransactions = transactionModel.ToList();
                        return View(uploadFIle);
                    }

                    try
                    {
                        var trasactioonUploadDbModel = transactionModel.Select(item =>
                        {
                            decimal.TryParse(item.Amount, out decimal amount);
                            var transactionDate = item.GetTransactionDateTime();
                            return new TransactionUpload
                            {

                                TransactionId = item.TransactionId.Trim(),
                                Amount = amount,
                                Currency = item.CurrencyCode.Trim(),
                                Status = item.Status.ToUpper().Trim(),
                                TransactionDate = transactionDate,
                                FromFile = item.FromFileType
                            };
                        });
                        _InvoiceDB.TransactionUpload.AddRange(trasactioonUploadDbModel);
                        _InvoiceDB.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        return View("Error");

                    }

                }

                return RedirectToAction("Success");
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        public IActionResult Success()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
