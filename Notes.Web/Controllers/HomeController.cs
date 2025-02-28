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

        public IActionResult GetLink() 
        {
            return Redirect("https://github.com/keksikls");
        }

    }
}
