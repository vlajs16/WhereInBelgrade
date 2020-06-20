using DataAccessLayer;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeedData
{
    public class Seed
    {

        public static void SeedEvents(BeogradContext context)
        {
            if (!context.Dogadjaji.Any())
            {
                var eventsData = System.IO.File.ReadAllText("../SeedData/Data/EventsSeedJson.json");
                var events = JsonConvert.DeserializeObject<List<Dogadjaj>>(eventsData);

                foreach (var ev in events)
                {
                    foreach (var dog in ev.KategorijeDogadjaji)
                    {
                        dog.Kategorija = context.Kategorije.FirstOrDefault(x => x.Naziv == dog.Kategorija.Naziv);
                    }
                    context.Dogadjaji.Add(ev);
                }
                context.SaveChanges();
            }
        }

        public static void SeedCategories(BeogradContext context)
        {
            if (!context.Kategorije.Any())
            {
                var categoryData = System.IO.File.ReadAllText("../SeedData/Data/CategorySeedJson.json");
                var categories = JsonConvert.DeserializeObject<List<Kategorija>>(categoryData);

                foreach (var cat in categories)
                {
                    context.Kategorije.Add(cat);
                }
                context.SaveChanges();
            }
        }

        public static void SeedUsers(BeogradContext context)
        {
            if (!context.Korisnici.Any())
            {
                var usersData = System.IO.File.ReadAllText("../SeedData/Data/UserSeedJson.json");
                var users = JsonConvert.DeserializeObject<List<Korisnik>>(usersData);

                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("1234", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    context.Korisnici.Add(user);
                }
                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }

    
}
