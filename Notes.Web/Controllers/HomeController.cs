using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
