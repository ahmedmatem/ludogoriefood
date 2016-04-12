namespace LudogorieFood.Web.ViewModels.Shop
{
    using System.ComponentModel.DataAnnotations;
    
    using Models.Products;
    using AutoMapper;
    using LudogorieFood.Web.Infrastructure.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string PictureName { get; set; }
        
        public float Amount { get; set; }
        
        public decimal Price { get; set; }

        public decimal NewPrice { get; set; }

        public bool InStock { get; set; }

        public float OverallRating { get; set; }
    }
}