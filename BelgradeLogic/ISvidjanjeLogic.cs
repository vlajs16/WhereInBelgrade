using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface ISvidjanjeLogic
    {
        Task<List<Svidjanje>> GetObjects();
        Task<bool> Insert(Svidjanje svidjanje);
        Task<bool> Delete(int korisnikId, int dogadjajId);
    }
}
