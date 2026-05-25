using Microsoft.AspNetCore.Mvc;

namespace BtkAkademiAIBlog.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
