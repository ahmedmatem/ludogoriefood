namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using ViewModels.Home;
    using Models;

    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var model = new HomePageViewModel()
            {
                Slides = new List<Slide>
                {
                    new Slide { Id = 1, Name = "test", PictureUrl = "~/Content/images/img1.jpg"},
                    new Slide { Id = 2, Name = "test", PictureUrl = "~/Content/images/img2.jpg"},
                    new Slide { Id = 3, Name = "test", PictureUrl = "~/Content/images/img2.jpg"},
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MoveTo(SlideViewModel model)
        {
            return this.View();
        }
    }
}