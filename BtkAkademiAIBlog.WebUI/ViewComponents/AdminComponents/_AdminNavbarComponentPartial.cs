using Microsoft.AspNetCore.Mvc;

namespace BtkAkademiAIBlog.WebUI.ViewComponents.AdminComponents
{
    public class _AdminNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
