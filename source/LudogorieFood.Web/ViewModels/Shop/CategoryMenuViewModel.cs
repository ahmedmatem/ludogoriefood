namespace LudogorieFood.Web.ViewModels.Shop
{
    using System.Collections.Generic;

    using LudogorieFood.Models.Products;

    public class CategoryMenuViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public IDictionary<int, int> NumberOfProductsInCategories { get; set; }
    }
}