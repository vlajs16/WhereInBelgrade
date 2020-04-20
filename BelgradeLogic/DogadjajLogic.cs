using DataAccessLayer;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public class DogadjajLogic : IDogadjajLogic
    {
        private BeogradContext _beogradContext;
        public DogadjajLogic(BeogradContext beogradContext)
        {
            _beogradContext = beogradContext;
        }
        public async Task<bool> Delete(int id)
        {
            Dogadjaj d = await _beogradContext.Dogadjaji.FirstOrDefaultAsync(x => x.DogadjajID == id);
            if (d == null)
                return false;
            _beogradContext.Dogadjaji.Remove(d);
            await _beogradContext.SaveChangesAsync();
            return true;
        }

        public async Task<Dogadjaj> Find(int id)
        {
            return await _beogradContext.Dogadjaji.FirstOrDefaultAsync(x => x.DogadjajID == id);
        }

        public async Task<PagedList<Dogadjaj>> GetObjects(EventParams eventParams)
        {
            var dogadjaji = _beogradContext.Dogadjaji.OrderByDescending(x => x.DogadjajID);
            return await PagedList<Dogadjaj>.CreateAsync(dogadjaji, eventParams.PageNumber, eventParams.PageSize);
        }

        public async Task<PagedList<Dogadjaj>> GetObjectsByKategorija(EventParams eventParams, string kategorija)
        {
            var dogadjaji =  _beogradContext.Dogadjaji.
                Where(p => p.KategorijeDogadjaji.Any(k => k.Kategorija.Naziv == kategorija))
                .OrderByDescending(x => x.DogadjajID);
            return await PagedList<Dogadjaj>.CreateAsync(dogadjaji, eventParams.PageNumber, eventParams.PageSize);
        }

        public async Task<List<Dogadjaj>> GetObjectsByKategorijaThree(string kategorija)
        {
            List<Dogadjaj> dogadjaji = await _beogradContext.Dogadjaji.
                Where(p => p.KategorijeDogadjaji.Any(k => k.Kategorija.Naziv == kategorija))
                .OrderByDescending(x => x.DogadjajID).Take(3).ToListAsync();
            return dogadjaji;
        }

        public async Task<bool> Insert(Dogadjaj dogadjaj)
        {
            try
            {
                if(dogadjaj.Lokacija != null)
                {
                     var mesto = await _beogradContext.Mesta.FirstOrDefaultAsync(x => x.MestoID == dogadjaj.Lokacija.MestoID);
                if (mesto != null)
                    dogadjaj.Lokacija = mesto;
                }

                _beogradContext.Dogadjaji.Add(dogadjaj);
                await _beogradContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Dogadjaj dogadjaj)
        {
            var d = await _beogradContext.Dogadjaji.FirstOrDefaultAsync(p => p.DogadjajID == dogadjaj.DogadjajID);
            if (d == null)
                return false;
            d.Naziv = dogadjaj.Naziv;
            d.Opis = dogadjaj.Opis;
            d.Lokacija = dogadjaj.Lokacija;
            d.Url = dogadjaj.Url;
            //foreach (var katDog in dogadjaj.KategorijeDogadjaji)
            //{
            //    katDog.Kategorija = await _beogradContext.Kategorije.FirstOrDefaultAsync(k => k.KategorijaID == katDog.KategorijaID);
            //    if(await _beogradContext.KategorijeDogadjaji.FirstOrDefaultAsync(k => k.DogadjajID == d.DogadjajID && k.KategorijaID == katDog.KategorijaID) == null)
            //        _beogradContext.KategorijeDogadjaji.Add(katDog);
            //}


            List<KategorijaDogadjaj> kategorijeDogadjaji = new List<KategorijaDogadjaj>();
            //d.KategorijeDogadjaji = kategorijeDogadjaji;
            //await _beogradContext.SaveChangesAsync();


            foreach (KategorijaDogadjaj katDog in dogadjaj.KategorijeDogadjaji)
            {
                kategorijeDogadjaji.Add(new KategorijaDogadjaj
                {
                    Kategorija =  _beogradContext.Kategorije.FirstOrDefault(k => k.KategorijaID == katDog.KategorijaID),
                    KategorijaID = (_beogradContext.Kategorije.FirstOrDefault(k => k.KategorijaID == katDog.KategorijaID)).KategorijaID,
                    DogadjajID = d.DogadjajID
                });
            }
            d.KategorijeDogadjaji = kategorijeDogadjaji;
            d.DatumPocetka = dogadjaj.DatumPocetka;
            d.DatumZavrsetka = dogadjaj.DatumZavrsetka;
            //_beogradContext.Dogadjaji.Update(d);
            try
            {
                await _beogradContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
