namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
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
            var slides = this.slides.All().Where(s => !s.IsDeleted).ToList();

            var linkedSlides = new LinkedSlideList(slides);

            var model = new HomePageViewModel()
            {
                Slides = linkedSlides.Slides,
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

            if (slideId == 0) 
                // There is not any slide yet. Just create new slide.
            {
                newSlide.PictureName = Helpers.CreateUniqueFileName(slide.FileName);
                newSlide.PictureType = Helpers.GetFileTypeFromName(slide.FileName).ConvertToPictureType();
            }
            else if(position == "after")
                // Create new slide after slide with given slideId
            {

            }

            slides.Add(newSlide);
            slides.Save();

            return RedirectToAction("Index");
        }
    }
}