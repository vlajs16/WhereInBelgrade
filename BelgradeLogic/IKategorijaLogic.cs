using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BelgradeLogic
{
    public interface IKategorijaLogic
    {
        List<Kategorija> GetObjects();
        bool Insert(Kategorija kategorija);
        bool Update(Kategorija kategorija);
        bool Delete(int id);
        Kategorija Find(int id);
    }
}
