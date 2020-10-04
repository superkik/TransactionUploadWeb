using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UploadTransaction.Models.DB;
using UploadTransaction.Models.ResponseAPI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UploadTransaction.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TransactionController : ControllerBase
    {

        private InvoiceContext _InvoiceDB;

        public TransactionController(InvoiceContext dbContext)
        {
            _InvoiceDB = dbContext;
        }


        // GET: api/<controller>
        [HttpGet]
        [Route("currency/{currency?}")]
        public IActionResult GetTransactionByCurrency(string currency)
        {
            var transaction = _InvoiceDB.TransactionUpload
                              .Where(item => !string.IsNullOrEmpty(currency) ? item.Currency.ToUpper().Contains(currency.ToUpper()) : true)
                              .Select(item => new TransactionResponse(item)).ToArray();
            if (transaction != null && transaction.Any())
            {
                return Ok(transaction);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("date/{startDateStr}/to/{enddateStr}")]
        public IActionResult GetTransactionByDateRange(string startDateStr, string endDateStr)
        {
            if (string.IsNullOrEmpty(startDateStr))
            {
                return StatusCode(500, "Start date is empty");
            }
            if (string.IsNullOrEmpty(endDateStr))
            {
                return StatusCode(500, "Start date is empty");
            }
            var enUS = new CultureInfo("en-US");
            if (!DateTime.TryParseExact(startDateStr, "dd-MM-yyyy", enUS,
                         DateTimeStyles.None, out DateTime startDate))
            {
                return StatusCode(500, "Start date is incorrect format. dd-MM-yyyy");
            }
            if (!DateTime.TryParseExact(endDateStr, "dd-MM-yyyy", enUS,
                         DateTimeStyles.None, out DateTime endDate))
            {
                return StatusCode(500, "End date is incorrect format. dd-MM-yyyy");

            }
            var transaction = _InvoiceDB.TransactionUpload
                              .Where(item => item.TransactionDate.Value.Date >= startDate && item.TransactionDate.Value.Date <= endDate)
                              .Select(item => new TransactionResponse(item)); 
            if (transaction != null && transaction.Any())
            {
                return Ok(transaction);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("status/{status?}")]
        public IActionResult GetTransactionByDateRange(string status)
        {
            var transaction = _InvoiceDB.TransactionUpload
                             .Where(item => !string.IsNullOrEmpty(status) ? item.Status.ToUpper().Equals(status.ToUpper()) : true)
                             .Select(item => new TransactionResponse(item));
            if (transaction != null && transaction.Any())
            {
                return Ok(transaction);
            }
            else
            {
                return Ok();
            }
        }
    }
}
