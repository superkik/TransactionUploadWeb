using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadTransaction.Models.InternalModels;

namespace UploadTransaction.Library.Mapper
{
    public sealed class TransactionMap : ClassMap<CSVTransactionItem>
    {
        public TransactionMap()
        {
            Map(x => x.TransactionId).Index(0);
            Map(x => x.Amount).Index(1);
            Map(x => x.CurrencyCode).Index(2);
            Map(x => x.TransactionDate).Index(3);
            Map(x => x.Status).Index(4);
        }
    }
}
