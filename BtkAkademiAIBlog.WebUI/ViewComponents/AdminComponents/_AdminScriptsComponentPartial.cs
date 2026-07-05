using Microsoft.AspNetCore.Mvc;

namespace BtkAkademiAIBlog.WebUI.ViewComponents.AdminComponents
{
    public class _AdminScriptsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
