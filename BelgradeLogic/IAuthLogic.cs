using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface IAuthLogic
    {
        Task<Korisnik> Register(Korisnik korisnik, string password);
        Task<Korisnik> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
