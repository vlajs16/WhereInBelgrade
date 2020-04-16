using Helpers;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface ISvidjanjeLogic
    {
        Task<PagedList<Svidjanje>> GetObjects(EventParams eventParams, int korisnikId);
        Task<bool> Insert(Svidjanje svidjanje);
        Task<bool> Delete(int korisnikId, int dogadjajId);
        Task<Korisnik> GetUser(int userId);
    }
}
