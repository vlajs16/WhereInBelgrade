using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface IKomentarLogic
    {
       

        Task<List<Komentar>> GetObjects();
        Task<bool> Insert(Komentar komentar);
        Task<bool> Delete(int korisnikId, int dogadjajId);







    }
}
