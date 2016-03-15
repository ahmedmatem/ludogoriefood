namespace LudogorieFood.Web.Areas.Admin.ViewModels.Home
{
    using System.Collections.Generic;

    using LudogorieFood.Models;

    public class HomePageViewModel
    {
        public ICollection<Slide> Slides { get; set; }

        public ICollection<Slide> DeletedSlides { get; set; }
    }
}