using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace UploadTransaction.Library.Helper
{
    public static class CurrencyHelper
    {
        public static bool IsExist(string ISOCurrencySymbol)
        {
            var symbol = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture => {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencySymbol)
                .Select(ri => ri.CurrencySymbol)
                .FirstOrDefault();
            return symbol != null;
        }

        //public static bool TryGetCurrencySymbol(string ISOCurrencySymbol, out string symbol)
        //{
        //    symbol = CultureInfo
        //        .GetCultures(CultureTypes.AllCultures)
        //        .Where(c => !c.IsNeutralCulture)
        //        .Select(culture => {
        //            try
        //            {
        //                return new RegionInfo(culture.Name);
        //            }
        //            catch
        //            {
        //                return null;
        //            }
        //        })
        //        .Where(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencySymbol)
        //        .Select(ri => ri.CurrencySymbol)
        //        .FirstOrDefault();
        //    return symbol != null;
        //}
    }
}
