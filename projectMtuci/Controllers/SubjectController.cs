using Microsoft.AspNetCore.Mvc;
using projectMtuci.DAL;
using projectMtuci.Domain.Entity;
using projectMtuci.Domain.ViewModels.Basket;
using projectMtuci.Service.Implementations;
using projectMtuci.Service.Interfaces;
using System.Security.Claims;

namespace projectMtuci.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly ISubjectService _subjectService;

        private readonly IBasketService _basketService;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;

        public SubjectController(ISubjectService subjectService, IBasketService basketService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _subjectService = subjectService;
            _basketService = basketService;
            this.environment = environment;
        }

        public IActionResult Physics()
        {
            return RedirectToRoute("default", new { controller = "Subject", action = "Index", SubjectName = "Физика" });
        }

        public IActionResult VvIT()
        {
            return RedirectToRoute("default", new { controller = "Subject", action = "Index", SubjectName = "ВвИТ" });
        }

        public IActionResult Devops()
        {
            return RedirectToRoute("default", new { controller = "Subject", action = "Index", SubjectName = "Devops" });
        }

        public IActionResult MathDb()
        {
            return RedirectToRoute("default", new { controller = "Subject", action = "Index", SubjectName = "MathDb" });
        }

        public IActionResult Index(string SubjectName)
        {
            var result = _subjectService.GetSubjectByName(SubjectName).Data.ToList();
            return View(result);
        }

        public IActionResult AddToBasket(int id)
        {
            var basketItem = new BasketViewModel()
            {
                UserName = User.Identity.Name,
                SubjectId = id
            };
            var basketItems = _basketService.GetBasketItems().Result.Data;

            if (basketItems == null)
            {
                _basketService.CreateBasketItem(basketItem);
                return RedirectToAction("Index", "Home");
            }

            foreach (var item in basketItems)
            {
                if (item.SubjectId == id && item.UserName.Trim() == User.Identity.Name.Trim())
                {
                    Console.WriteLine(21344);
                    return RedirectToAction("Index", "Home");
                }
            }
            _basketService.CreateBasketItem(basketItem);

            return RedirectToRoute("default", new { controller = "Home", action = "Index" });
        }

        public IActionResult Basket(string userName)
        {
            var result = new List<Subject>();
            //null check
            if (_basketService.GetBasketItems().Result.Data == null)
            {
                return View(result);
            }
            var basketItems = _basketService.GetBasketItems().Result.Data.Where(x => x.UserName.Trim() == userName.Trim());
            var subjects = _subjectService.GetSubjects().Result.Data;
            var ids = new List<int>();
            

            if (subjects == null)
            {
                return View(result);
            }

            foreach (var item in subjects)
            {
                ids.Add(item.Id);
            }
            foreach (var item in basketItems)
            {
                if (ids.Contains(item.SubjectId))
                {
                    result.Add(_subjectService.GetSubject(item.SubjectId).Result.Data);
                }
            }
            
            return View(result);
        }

        public IActionResult DeleteFromBasket(int id)
        {
            var basketItem = _basketService.GetBasketItems().Result.Data.Where(x => x.SubjectId == id).Where(x => x.UserName.Trim() == User.Identity.Name.Trim());

            _basketService.DeleteBasketItem(basketItem.First().Id);

            return RedirectToRoute("default", new { controller = "Subject", action = "Basket", userName = User.Identity.Name });
        }

        public FileContentResult DownloadSubject(string path)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(this.environment.WebRootPath, "../uploads/") + path);

            return File(bytes, "application/octet-stream", path);
        }
    }
}
