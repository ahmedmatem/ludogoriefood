namespace LudogorieFood.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using LudogorieFood.Models;

    public class HomePageViewModel
    {
        public ICollection<Slide> Slides { get; set; }
    }
}