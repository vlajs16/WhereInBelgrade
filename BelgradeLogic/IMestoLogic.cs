using Model;
using System;
using System.Collections.Generic;

namespace BelgradeLogic
{
    public interface IMestoLogic
    {
        List<Mesto> GetObjects();
        bool Insert(Mesto mesto);
        bool Update(Mesto mesto);
        bool Delete(int id);
        Mesto Find(int id);

    }
}
