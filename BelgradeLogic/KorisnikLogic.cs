using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public class KorisnikLogic : IKorisnikLogic
    {
        private BeogradContext _belgradeContext;

        public KorisnikLogic(BeogradContext beogradContext)
        {
            _belgradeContext = beogradContext;
        }
        
        public async Task<Korisnik> FindUser(int userId)
        {
            return await _belgradeContext.Korisnici.FirstOrDefaultAsync(x => x.KorisnikID == userId);
        }

        public async Task<bool> IsAdmin(int userId)
        {
            var userFromDb = await _belgradeContext.Korisnici.FirstOrDefaultAsync(x => x.KorisnikID == userId);
            return userFromDb.Admin;
        }
    }
}
