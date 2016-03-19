namespace LudogorieFood.Web.Areas.Admin.ViewModels.Home
{
    using System.Web;
    using System.Collections.Generic;

    using LudogorieFood.Models;
    using Infrastructure.ValidationAttributes;

    public class HomePageViewModel
    {
        public ICollection<Slide> Slides { get; set; }

        public ICollection<Slide> DeletedSlides { get; set; }

        [FileExtensions("jpeg,jpg,gif,png")]
        public HttpPostedFileBase Slide { get; set; }
    }
}