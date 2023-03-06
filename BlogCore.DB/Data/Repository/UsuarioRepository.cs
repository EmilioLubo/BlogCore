using BlogCore.Data;
using BlogCore.DB.Data.Repository.IRepository;
using BlogCoreModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.DB.Data.Repository
{
    public class UsuarioRepository : Repository<AppUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var dbUser = _db.AppUsers.FirstOrDefault(u => u.Id == IdUsuario);
            dbUser.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var dbUser = _db.AppUsers.FirstOrDefault(u => u.Id == IdUsuario);
            dbUser.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
