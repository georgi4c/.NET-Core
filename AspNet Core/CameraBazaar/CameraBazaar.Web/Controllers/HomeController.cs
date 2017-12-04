using CameraBazaar.Web.Models;
using CameraBazaar.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CameraBazaar.Web.Controllers
{
    [Log]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
