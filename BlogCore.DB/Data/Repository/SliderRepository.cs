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
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;
        public SliderRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Slider slider)
        {
            var dbObject = _db.Sliders.FirstOrDefault(i => i.Id == slider.Id);
            dbObject.Nombre = slider.Nombre;
            dbObject.Estado = slider.Estado;
            dbObject.UrlImagen = slider.UrlImagen;
            _db.SaveChanges();
        }
    }
}
