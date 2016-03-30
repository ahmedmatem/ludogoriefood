namespace LudogorieFood.Web.Areas.Admin.ViewModels.Shop
{
    using System.ComponentModel.DataAnnotations;

    using Models.Types;
    using Infrastructure.Mapping;
    using Models.Products;

    public class ProductViewModel : IMapFrom<Product>
    {
        [Required]
        public string Name { get; set; }

        public string PictureName { get; set; }

        [Required]
        public MeasureType Measure { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal NewPrice { get; set; }

        public bool InStock { get; set; }
    }
}