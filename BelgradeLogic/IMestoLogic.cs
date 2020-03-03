using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface IMestoLogic
    {
        Task<List<Mesto>> GetObjects();
        Task<bool> Insert(Mesto mesto);
        Task<bool> Update(Mesto mesto);
        Task<bool> Delete(int id);
        Task<Mesto> Find(int id);

    }
}
