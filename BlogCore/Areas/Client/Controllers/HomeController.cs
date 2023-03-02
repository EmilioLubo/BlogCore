using BlogCore.DB.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCoreModels;
using BlogCoreModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public HomeController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM()
            {
                Slider = _workContainer.Slider.GetAll(),
                ListaArticulos = _workContainer.Articulo.GetAll()
            };

            ViewBag.IsHome = true;

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            Articulo dbArt = _workContainer.Articulo.Get(id);
            
            return View(dbArt);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}