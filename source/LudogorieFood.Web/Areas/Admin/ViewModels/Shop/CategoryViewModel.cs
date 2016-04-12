namespace LudogorieFood.Web.Areas.Admin.ViewModels.Shop
{

    using System.ComponentModel.DataAnnotations;

    using Infrastructure.Mapping;
    using Models.Products;

    public class CategoryViewModel : IMapFrom<Category>, IMapTo<Category>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}