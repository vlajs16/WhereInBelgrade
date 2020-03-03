using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Model;

namespace BelgradeLogic
{
    public class AuthLogic : IAuthLogic
    {
        private readonly BeogradContext _context;

        public AuthLogic(BeogradContext context)
        {
            _context = context;
        }

        public async Task<Korisnik> Login(string username, string password)
        {
            var korisnik = await _context.Korisnici.FirstOrDefaultAsync(x => x.Username == username);

            if (korisnik == null)
                return null;

            if (!VerifyPasswordHash(password, korisnik.PasswordHash, korisnik.PasswordSalt))
                return null;

            return korisnik;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

        public async Task<Korisnik> Register(Korisnik korisnik, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            korisnik.PasswordHash = passwordHash;
            korisnik.PasswordSalt = passwordSalt;

            await _context.Korisnici.AddAsync(korisnik);
            await _context.SaveChangesAsync();

            return korisnik;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Korisnici.AnyAsync(x => x.Username == username))
                return true;
            return false;
        }
    }
}
