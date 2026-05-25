using Microsoft.AspNetCore.Mvc;

namespace BtkAkademiAIBlog.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
