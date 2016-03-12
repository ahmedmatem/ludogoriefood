namespace LudogorieFood.Models
{
    using LudogorieFood.Web.Common.Models;

    public class Slide : BaseModel<int>
    {
        public string PictureName { get; set; }

        public PictureType PictureType { get; set; }

        public int? NextSlideId { get; set; }

        public int? PrevSlideId { get; set; }
    }
}
