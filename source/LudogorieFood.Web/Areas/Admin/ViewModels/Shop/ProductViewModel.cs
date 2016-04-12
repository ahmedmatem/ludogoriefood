namespace LudogorieFood.Web.Areas.Admin.ViewModels.Shop
{
    using System.Web;
    using System.ComponentModel.DataAnnotations;

    using Models.Types;
    using Models.Products;

    using Infrastructure.Mapping;
    using AutoMapper;
    using System;
    using Common;

    public class ProductViewModel : IMapFrom<Product>, IMapTo<Product>, IHaveCustomMappings
    {
        public ProductViewModel()
        {
            this.PictureName = GlobalConstants.DefaultNoPictureName;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Infrastructure.ValidationAttributes.FileExtensions("jpeg,jpg,png,gif")]
        public HttpPostedFileBase Picture { get; set; }

        public string PictureName { get; set; }

        [Required]
        public MeasureType Measure { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal NewPrice { get; set; }

        public bool InStock { get; set; }
        
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name));
        }
    }
}