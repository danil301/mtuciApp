using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using projectMtuci.Domain.Entity;
using projectMtuci.Domain.ViewModels.Subject;
using projectMtuci.Service.Helpers;
using projectMtuci.Service.Interfaces;
using System.Dynamic;

namespace projectMtuci.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISubjectService _subjectService;

        private readonly IBasketService _basketService;

        private IWebHostEnvironment environment;

        public AdminController(ISubjectService subjectService, IWebHostEnvironment environment, IBasketService basketService)
        {
            _subjectService = subjectService;
            this.environment = environment;
            _basketService = basketService;
        }



        //[Authorize(Roles = "Admin     ")]
        public IActionResult AdminPanel()
        {            
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            //check null
            if (_subjectService.GetSubjects().Result.Data != null)
            {
                var result = _subjectService.GetSubjects().Result.Data.ToList();
                return View(result);
            }
            else return View(new List<Subject> { });
            
        }       

        [HttpGet]
        public IActionResult Add()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSubjectViewModel subject)
        {
            var Subject = new SubjectViewModel()
            {
                Name = subject.Name,
                Course = subject.Course,
                Degree = subject.Degree,
                PathToFile = subject.Upload.FileName
            };


            var file = Path.Combine(environment.ContentRootPath, "uploads", subject.Upload.FileName);
            try
            {
                using (var filestream = new FileStream(file, FileMode.Create))
                {
                    await subject.Upload.CopyToAsync(filestream);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            _subjectService.CreateSubject(Subject);
            
            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _subjectService.GetSubject(id).Result.Data;
            return View(result);
        }

        public IActionResult Delete(int id)
        {
            var helper = new FileHelper();
            helper.FileDeleteFromFoler(_subjectService.GetSubject(id).Result.Data.PathToFile);



            _subjectService.DeleteSubject(id);

            try
            {
                var basketItemsIds = _basketService.GetBasketItems().Result.Data.ToList();
                foreach (var item in basketItemsIds)
                {
                    if (item.SubjectId == id) _basketService.DeleteBasketItem(item.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            

            return RedirectToAction("List", "Admin");
        }
    }
}
