﻿using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface IKorisnikLogic
    {
        Task<Korisnik> FindUser(int userId);
        Task<bool> IsAdmin(int userId);
    }
}
