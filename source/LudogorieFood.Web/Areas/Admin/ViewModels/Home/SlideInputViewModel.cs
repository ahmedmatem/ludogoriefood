namespace LudogorieFood.Web.Areas.Admin.ViewModels.Home
{
    using Infrastructure.ValidationAttributes;
    using System.Web;

    public class SlideInputViewModel
    {
        public int SlideId { get; set; }

        [FileExtensions("jpeg,jpg,png,gif")]
        public HttpPostedFileBase Slide { get; set; }

        public string Position { get; set; }
    }
}