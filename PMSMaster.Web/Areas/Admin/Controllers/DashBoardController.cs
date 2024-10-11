using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            var userName = Convert.ToString(Request.Cookies["UserName"]);

            if (!string.IsNullOrWhiteSpace(userName))
                ViewBag.UserName = userName.ToUpper();
            else
                ViewBag.UserName = "";

            return View();
        }
    }
}
