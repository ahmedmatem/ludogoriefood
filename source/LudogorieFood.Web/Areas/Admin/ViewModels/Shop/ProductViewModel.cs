namespace LudogorieFood.Web.Areas.Admin.ViewModels.Shop
{
    using System.Web;
    using System.ComponentModel.DataAnnotations;

    using Models.Types;
    using Models.Products;

    using Infrastructure.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Display(Name="Category")]
        public int CategoryId { get; set; }

        [Infrastructure.ValidationAttributes.FileExtensions("jpeg,jpg,png,gif")]
        public HttpPostedFileBase Picture { get; set; }

        [Required]
        public MeasureType Measure { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal NewPrice { get; set; }

        public bool InStock { get; set; }
    }
}