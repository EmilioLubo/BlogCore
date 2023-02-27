using BlogCore.Data;
using BlogCore.DB.Data.Repository.IRepository;
using BlogCoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogCore.DB.Data.Repository
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;
        public ArticuloRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Articulo articulo)
        {
            var dbObject = _db.Articulos.FirstOrDefault(i => i.Id == articulo.Id);
            dbObject.Nombre = articulo.Nombre;
            dbObject.Descripcion = articulo.Descripcion;
            dbObject.UrlImagen = articulo.UrlImagen;
            dbObject.CategoriaId = articulo.CategoriaId;
            _db.SaveChanges();
        }
    }
}
