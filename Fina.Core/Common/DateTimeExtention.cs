using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Core.Common
{
    public static class DateTimeExtention
    {
        public static DateTime GetFirstDay(this DateTime date, int? year, int? month)
        {
            DateTime retorno = new DateTime(year ?? date.Year, month ?? date.Month, 1);
            return retorno;
        }

        public static DateTime GetLastDay(this DateTime date, int? year, int? month)
        {
            DateTime retorno = new DateTime(year ?? date.Year, month ?? date.Month, 1);
            retorno.AddMonths(1);
            retorno.AddDays(-1);
            return retorno;
        }
    }
}
