using BlogCore.Data;
using BlogCore.DB.Data.Repository.IRepository;
using BlogCoreModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IWorkContainer _workContainer;
       
        public CategoriasController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
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
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            { 
                _workContainer.Categoria.Add(categoria);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();
            categoria = _workContainer.Categoria.Get(id);
            if(categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _workContainer.Categoria.Update(categoria);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #region LLamadasAPI
        [HttpGet]
        public IActionResult GetAll() 
        {
            return Json(new { data = _workContainer.Categoria.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var dbObj = _workContainer.Categoria.Get(id);
            if(dbObj == null)
            {
                return Json(new {success = false, message = "Error al eliminar la categoría"});
            }

            _workContainer.Categoria.Remove(dbObj);
            _workContainer.Save();
            return Json(new { success = true, message = "Categoría eliminada satisfactoriamente" });
        }
        #endregion
    }
}
