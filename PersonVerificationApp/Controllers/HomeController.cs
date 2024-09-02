using Microsoft.AspNetCore.Mvc;
using PersonVerificationApp.Model;
using System.Diagnostics;

namespace PersonVerificationApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "TC Kimlik Numarası Doğrulama";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
