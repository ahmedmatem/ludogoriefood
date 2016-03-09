namespace LudogorieFood.Models
{
    using LudogorieFood.Web.Common.Models;

    public class Slide : BaseModel<int>
    {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public int Next { get; set; }

        public int Prev { get; set; }
    }
}
