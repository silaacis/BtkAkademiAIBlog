using Microsoft.AspNetCore.Mvc;

namespace BtkAkademiAIBlog.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
