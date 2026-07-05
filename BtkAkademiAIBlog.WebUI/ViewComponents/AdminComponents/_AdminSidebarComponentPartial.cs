using Microsoft.AspNetCore.Mvc;

namespace BtkAkademiAIBlog.WebUI.ViewComponents.AdminComponents
{
    public class _AdminSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
