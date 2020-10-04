using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadTransaction.Library.SystemConst
{
    public static class TransactionCSVStatusCodeConst
    {
        public const  string Approved = "Approved";
        public const  string Failed = "Failed";
        public const  string Finished = "Finished";


        public static bool IsExist(string statusCode)
        {
            var array = new string[] { Approved.ToUpper(), Failed.ToUpper(), Finished.ToUpper() };
            if (array.Contains(statusCode.ToUpper()))
            {
                return true;
            }
            return false;
        }
    }
}
