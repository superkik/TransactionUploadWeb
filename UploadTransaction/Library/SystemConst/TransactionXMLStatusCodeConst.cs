using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadTransaction.Library.SystemConst
{
    public static class TransactionXMLStatusCodeConst
    {
        public readonly static string Approved = "Approved";
        public readonly static string Rejected = "Rejected";
        public readonly static string Done = "Done";


        public static bool IsExist(string statusCode)
        {
            var array = new string[] { Approved.ToUpper(), Rejected.ToUpper(), Done.ToUpper() };
            if (array.Contains(statusCode.ToUpper()))
            {
                return true;
            }
            return false;
        }
    }
}
