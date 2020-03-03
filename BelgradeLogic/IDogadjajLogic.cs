﻿using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface IDogadjajLogic
    {
        Task<List<Dogadjaj>> GetObjects();
        Task<bool> Insert(Dogadjaj dogadjaj);
        Task<bool> Update(Dogadjaj dogadjaj);
        Task<bool> Delete(int id);
        Task<Dogadjaj> Find(int id);
    }
}