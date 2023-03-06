using BlogCore.Data;
using BlogCore.DB.Data.Repository.IRepository;
using BlogCoreModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public ArticulosController(IWorkContainer workContainer, IWebHostEnvironment webHostEnvironment)
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
            ArticuloVM vm = new ArticuloVM()
            {
                Articulo = new BlogCoreModels.Articulo(),
                ListaCategorias = _workContainer.Categoria.GetListaCategorias()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM vm)
        {
            if(ModelState.IsValid)
            {
                string route = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if(vm.Articulo.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(route, @"imagenes/articulos");

                    if(archivos.Count() > 0)
                    {
                        var extension = Path.GetExtension(archivos[0].FileName);

                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStreams);
                        }

                        vm.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;

                    }
                    else
                    {
                        vm.Articulo.UrlImagen = @"\imagenes\articulos\noimage.png";
                    }
                    vm.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _workContainer.Articulo.Add(vm.Articulo);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
            }
            vm.ListaCategorias = _workContainer.Categoria.GetListaCategorias();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM vm = new ArticuloVM()
            {
                Articulo = new BlogCoreModels.Articulo(),
                ListaCategorias = _workContainer.Categoria.GetListaCategorias()
            };

            if(id != null)
            {
                vm.Articulo = _workContainer.Articulo.Get(id.GetValueOrDefault());
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM vm)
        {
            if (ModelState.IsValid)
            {
                string route = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var articuloDb = _workContainer.Articulo.Get(vm.Articulo.Id);

                if (archivos.Count() > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(route, @"imagenes/articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var rutaImg = Path.Combine(route, articuloDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImg))
                    {
                        System.IO.File.Delete(rutaImg);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    vm.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    vm.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _workContainer.Articulo.Update(vm.Articulo);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    vm.Articulo.UrlImagen = articuloDb.UrlImagen;
                }
                _workContainer.Articulo.Update(vm.Articulo);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        #region LLamadasAPI
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workContainer.Articulo.GetAll(includeProperties: "Categoria") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var articuloDb = _workContainer.Articulo.Get(id);
            string route = _webHostEnvironment.WebRootPath;
            var rutaImg = Path.Combine(route, articuloDb.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImg))
            {
                System.IO.File.Delete(rutaImg);
            }

            if(articuloDb == null)
            {
                return Json(new {success = false, message = "Error al borrar el artículo"});
            }

            _workContainer.Articulo.Remove(articuloDb);
            _workContainer.Save();
            return Json(new { success = true, message = "Artículo eliminado satisfactoriamente" });
        }
        #endregion
    }
}
