using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;

namespace Helpers
{
    public static class Extensions
    {
        public static void AddAplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalculateDays(this DateTime date)
        {
            DateTime today = DateTime.Now;
            int days = (date - today).Days;
            return days;
        }

        public static List<Kategorija> ConvertToKategorije(this List<KategorijaDogadjaj> lista)
        {
            List<Kategorija> noveKategorije = new List<Kategorija>();
            foreach (var k in lista)
            {
                noveKategorije.Add(k.Kategorija);
            }
            return noveKategorije;
        }
    }
}
