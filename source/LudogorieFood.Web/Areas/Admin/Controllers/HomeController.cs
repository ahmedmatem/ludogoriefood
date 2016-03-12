namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using ViewModels.Home;
    using Models;
    using Common;
    using Infrastructure;

    public class HomeController : Controller
    {
        protected IDbRepository<Slide> slides;

        public HomeController(IDbRepository<Slide> slides)
        {
            this.slides = slides;
        }

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

        // TODO: Create [FileType(attribute)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewSlide(HttpPostedFileBase slide, string position, int slideId)
        {
            var newSlide = new Slide();

            if (slideId == 0) // there is not any slide
            {
                newSlide.Name = Helpers.CreateUniqueFileName(slide.FileName);
                newSlide.PictureType = Helpers.GetFileTypeFromName(slide.FileName).ConvertToPictureType();
                // TODO: check file path
                newSlide.PictureUrl = HttpContext.Server.MapPath("~/Content/Images/Home");
            }
            else if(position == "after")
            {

            }

            slides.Add(newSlide);
            slides.Save();

            return RedirectToAction("Index");
        }
    }
}