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
            var allWithDeleted = this.slides.AllWithDeleted().ToList();

            var activeSlides = allWithDeleted.Where(s => !s.IsDeleted).ToList();
            var deletedSlides = allWithDeleted.Where(s => s.IsDeleted).ToList();

            var linkedSlides = new LinkedSlideList(activeSlides);

            var model = new HomePageViewModel()
            {
                Slides = linkedSlides.Slides,
                DeletedSlides = deletedSlides,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MoveTo(SlideViewModel model)
        {
            var targetSlide = this.slides.GetById(model.Id);

            // move to the left
            if (model.Motion == SlideMotionType.MoveLeft)
            {
                Slide prevToprevSlide, nextSlide;
                var prevSlide = this.slides.GetById((int)targetSlide.PrevSlideId);

                targetSlide.PrevSlideId = prevSlide.PrevSlideId;
                if(prevSlide.PrevSlideId != null)
                {
                    prevToprevSlide = this.slides.GetById((int)prevSlide.PrevSlideId);
                    prevToprevSlide.NextSlideId = targetSlide.Id;
                }

                prevSlide.NextSlideId = targetSlide.NextSlideId;
                if(targetSlide.NextSlideId != null)
                {
                    nextSlide = this.slides.GetById((int)targetSlide.NextSlideId);
                    nextSlide.PrevSlideId = prevSlide.Id;
                }

                prevSlide.PrevSlideId = targetSlide.Id;
                targetSlide.NextSlideId = prevSlide.Id;
            }

            // move to the right
            if (model.Motion == SlideMotionType.MoveRight)
            {
                Slide nextToNextSlide, prevSlide;
                var nextSlide = this.slides.GetById((int)targetSlide.NextSlideId);

                targetSlide.NextSlideId = nextSlide.NextSlideId;
                if(nextSlide.NextSlideId != null)
                {
                    nextToNextSlide = this.slides.GetById((int)nextSlide.NextSlideId);
                    nextToNextSlide.PrevSlideId = targetSlide.Id;
                }

                nextSlide.PrevSlideId = targetSlide.PrevSlideId;
                if(targetSlide.PrevSlideId != null)
                {
                    prevSlide = this.slides.GetById((int)targetSlide.PrevSlideId);
                    prevSlide.NextSlideId = nextSlide.Id;
                }

                nextSlide.NextSlideId = targetSlide.Id;
                targetSlide.PrevSlideId = nextSlide.Id;
            }

            // delete
            if(model.Motion == SlideMotionType.Delete)
            {
                Slide prevSlide, nextSlide;

                if(targetSlide.PrevSlideId != null)
                {
                    prevSlide = this.slides.GetById((int)targetSlide.PrevSlideId);
                    prevSlide.NextSlideId = targetSlide.NextSlideId;
                }

                if(targetSlide.NextSlideId != null)
                {
                    nextSlide = this.slides.GetById((int)targetSlide.NextSlideId);
                    nextSlide.PrevSlideId = targetSlide.PrevSlideId;
                }

                this.slides.Delete(targetSlide);
            }

            if(model.Motion == SlideMotionType.HardDelete)
            {
                targetSlide = this.slides.AllWithDeleted().FirstOrDefault(s => s.Id == model.Id);
                this.slides.HardDelete(targetSlide);
            }

            if(model.Motion == SlideMotionType.Restore)
            {
                var lastSlide = this.slides.All().FirstOrDefault(s => s.NextSlideId == null);
                targetSlide = this.slides.AllWithDeleted().FirstOrDefault(s => s.Id == model.Id);
                lastSlide.NextSlideId = targetSlide.Id;

                targetSlide.PrevSlideId = lastSlide.Id;
                targetSlide.NextSlideId = null;
                targetSlide.IsDeleted = false;
            }

            this.slides.Save();

            return RedirectToAction("Index");
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
                newSlide.PictureName = Helpers.CreateUniqueFileName(slide.FileName);
                newSlide.PictureType = Helpers.GetFileTypeFromName(slide.FileName).ConvertToPictureType();
                slides.Add(newSlide);
                slides.Save();

                // get last inserted slide by CreatedOn field
                newSlide = this.slides.All().OrderByDescending(s => s.CreatedOn).FirstOrDefault();
                                
                Slide currentSlide, nextSlide, prevSlide;

                if (position == "after")
                // Create new slide after slide with given slideId
                {
                    currentSlide = this.slides.GetById(slideId);
                    if (currentSlide.NextSlideId == null)
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
                    currentSlide = this.slides.GetById(slideId);
                    if(currentSlide.PrevSlideId == null)
                    {
                        currentSlide.PrevSlideId = newSlide.Id;
                        newSlide.NextSlideId = currentSlide.Id;
                    }
                    else
                    {
                        prevSlide = this.slides.GetById((int)currentSlide.PrevSlideId);
                        currentSlide.PrevSlideId = newSlide.Id;
                        newSlide.NextSlideId = currentSlide.Id;
                        prevSlide.NextSlideId = newSlide.Id;
                        newSlide.PrevSlideId = prevSlide.Id;
                    }
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