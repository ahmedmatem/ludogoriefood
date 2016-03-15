namespace LudogorieFood.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Common;
    using Models;
    using ViewModels.Home;
    using Infrastructure;

    public class HomeController : Controller
    {
        public IDbRepository<Slide> slides;

        public HomeController(IDbRepository<Slide> slides)
        {
            this.slides = slides;
        }

        public ActionResult Index()
        {
            var activeSlides = this.slides.All().ToList();
            var linkedSlides = new LinkedSlideList(activeSlides);

            var model = new HomePageViewModel
            {
                Slides = linkedSlides.Slides,
            };

            return View(model);
        }
    }
}