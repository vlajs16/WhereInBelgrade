using Helpers;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface IDogadjajLogic
    {
        Task<PagedList<Dogadjaj>> GetObjects(EventParams userParams);
        Task<PagedList<Dogadjaj>> GetObjectsByKategorija(EventParams userParams, string kategorija);
        Task<List<Dogadjaj>> GetObjectsByKategorijaThree(string kategorija);
        Task<bool> Insert(Dogadjaj dogadjaj);
        Task<bool> Update(Dogadjaj dogadjaj);
        Task<bool> Update2(Dogadjaj dogadjaj);
        Task<bool> Delete(int id);
        Task<Dogadjaj> Find(int id);
    }
}
