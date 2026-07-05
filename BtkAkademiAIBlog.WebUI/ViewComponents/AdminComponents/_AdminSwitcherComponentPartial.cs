using Microsoft.AspNetCore.Mvc;

namespace BtkAkademiAIBlog.WebUI.ViewComponents.AdminComponents
{
    public class _AdminSwitcherComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
