﻿using BlogCore.Data;
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
    public class CategoriaRepositorio : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoriaRepositorio(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categorias.Select(i => new SelectListItem()
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                }
            );
        }

        public void Update(Categoria categoria)
        {
            var dbObject = _db.Categorias.FirstOrDefault(i => i.Id == categoria.Id);
            dbObject.Nombre = categoria.Nombre;
            dbObject.Orden = categoria.Orden;
            _db.SaveChanges();
        }
    }
}
