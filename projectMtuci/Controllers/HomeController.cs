using Microsoft.AspNetCore.Mvc;
using projectMtuci.DAL.Interfaces;
using projectMtuci.Domain.Entity;
using projectMtuci.Domain.ViewModels.Subject;
using projectMtuci.Domain.ViewModels.User;
using projectMtuci.Models;
using projectMtuci.Service.Interfaces;
using System.Diagnostics;

namespace projectMtuci.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly IUserService _userService;

        public HomeController(ISubjectService subjectService, IUserService userService)
        {
            _subjectService = subjectService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}