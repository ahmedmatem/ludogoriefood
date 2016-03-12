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
    using System.IO;

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
            if (slide != null && slide.ContentLength > 0)
            {
                // add new slides to database
                var newSlide = new Slide();
                slides.Add(newSlide);
                slides.Save();

                // get last inserted slide by CreateOn field
                newSlide = this.slides.All().OrderByDescending(s => s.CreatedOn).FirstOrDefault();

                newSlide.PictureName = Helpers.CreateUniqueFileName(slide.FileName);
                newSlide.PictureType = Helpers.GetFileTypeFromName(slide.FileName).ConvertToPictureType();

                Slide currentSlide, nextSlide;

                if (position == "after")
                // Create new slide after slide with given slideId
                {
                    currentSlide = this.slides.GetById(slideId);
                    if (currentSlide.NextSlideId == null)
                    // if it is the last slide
                    {
                        currentSlide.NextSlideId = newSlide.Id;
                        newSlide.PrevSlideId = currentSlide.Id;
                    }
                    else
                    {
                        nextSlide = this.slides.GetById((int)currentSlide.NextSlideId);
                        currentSlide.NextSlideId = newSlide.Id;
                        newSlide.PrevSlideId = currentSlide.Id;
                        nextSlide.PrevSlideId = newSlide.Id;
                        newSlide.NextSlideId = nextSlide.Id;
                    }
                }
                else if(position == "before")
                {

                }
                
                // store the slide picture inside ~/Content/Images/Home folder
                var path = Path.Combine(Server.MapPath(GlobalConstants.SliderImagesPath), newSlide.PictureName + "." + newSlide.PictureType);
                slide.SaveAs(path);
                
                // save changes to database
                slides.Save();
            }
            
            return RedirectToAction("Index");
        }

    }
}