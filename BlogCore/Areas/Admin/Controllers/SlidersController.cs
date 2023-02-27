using BlogCore.Data;
using BlogCore.DB.Data.Repository.IRepository;
using BlogCoreModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public SlidersController(IWorkContainer workContainer, IWebHostEnvironment webHostEnvironment)
        {
            _workContainer = workContainer;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if(ModelState.IsValid)
            {
                string route = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;


                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(route, @"imagenes/sliders");

                    if(archivos.Count() > 0)
                    {
                        var extension = Path.GetExtension(archivos[0].FileName);

                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStreams);
                        }

                        slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                    }
                    else
                    {
                        slider.UrlImagen = @"\imagenes\sliders\noimage.png";
                    }

                    _workContainer.Slider.Add(slider);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id != null)
            {
                var slider = _workContainer.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string route = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var sliderDb = _workContainer.Slider.Get(slider.Id);

                if (archivos.Count() > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(route, @"imagenes/sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var rutaImg = Path.Combine(route, sliderDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImg))
                    {
                        System.IO.File.Delete(rutaImg);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                    _workContainer.Slider.Update(slider);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.UrlImagen = sliderDb.UrlImagen;
                }
                _workContainer.Slider.Update(slider);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        #region LLamadasAPI
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workContainer.Slider.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var sliderDb = _workContainer.Slider.Get(id);
            
            if(sliderDb == null)
            {
                return Json(new {success = false, message = "Error al borrar el slider"});
            }

            _workContainer.Slider.Remove(sliderDb);
            _workContainer.Save();
            return Json(new { success = true, message = "Slider eliminado satisfactoriamente" });
        }
        #endregion
    }
}
