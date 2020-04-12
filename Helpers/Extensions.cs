using DataAccessLayer;
using DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

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

        public static List<KategorijaDogadjaj> ConvertToEvent(List<Kategorija> kategorije, int id)
        {
            List<KategorijaDogadjaj> kategorijeDogadjaji = new List<KategorijaDogadjaj>();
            foreach (var kategorija in kategorije)
            {
                kategorijeDogadjaji.Add(new KategorijaDogadjaj
                {
                    Kategorija = new Kategorija 
                    {
                        KategorijaID = kategorija.KategorijaID,
                        Naziv = kategorija.Naziv
                    },
                    KategorijaID = kategorija.KategorijaID,
                    DogadjajID = id,
                    Dogadjaj = new Dogadjaj { DogadjajID = id }
                });
            }
            return kategorijeDogadjaji;
        }

        public static  byte[] GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }


    }
}
