using Helpers;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface IKategorijaLogic
    {
        Task<List<Kategorija>> GetObjects();
        Task<PagedList<Dogadjaj>> GetObjectsByKategorija(EventParams eventParams, string kategorija);
        Task<bool> Insert(Kategorija kategorija);
        Task<bool> Update(Kategorija kategorija);
        Task<bool> Delete(int id);
        Task<Kategorija> Find(int id);
        Task<List<Dogadjaj>> GetAllEvents();

    }
}
