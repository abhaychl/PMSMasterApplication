using PMSMaster.Entity.Models;
using PMSMaster.OpenIdDict.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PMSMaster.OpenIdDict.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IUserRepository _userRepository;

        //public HomeController(IUserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}

        public IActionResult Index()
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

        //[HttpGet]
        //public Users GetUserByClientSecret(string clientID)
        //{
        //    return _userRepository.GetUserByClientSecret(clientID);
        //}
    }
}