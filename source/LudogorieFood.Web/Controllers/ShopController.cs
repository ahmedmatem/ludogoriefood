namespace LudogorieFood.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using Models.Products;
    using Common;
    using ViewModels.Shop;
    using Infrastructure.Mapping;

    public class ShopController : Controller
    {
        private readonly IDbRepository<Category> categories;
        private readonly IDbRepository<Product> products;

        private Dictionary<int, int> numberOfProductsInCategories;

        public ShopController(IDbRepository<Category> categories, IDbRepository<Product> products)
        {
            this.categories = categories;
            this.products = products;

            this.CalculateNumberOfProductsInCategories();

            ViewBag.Categories = this.categories.All().ToList();
            ViewBag.NumberOfProductsInCategories = this.numberOfProductsInCategories;
        }
        
        // GET: shop/
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: shop/products/categoryID
        public ActionResult CategoryProducts(int id)
        {
            ViewBag.CategoryId = id;

            var model = this.products
                .All()
                .Where(p => p.CategoryId == id)
                .To<ProductViewModel>()
                .ToList();

            return this.View(model);
        }

        private void CalculateNumberOfProductsInCategories()
        {
            this.numberOfProductsInCategories = new Dictionary<int, int>();

            var groupedProducts = this.products
               .All()
               .GroupBy(p => p.CategoryId);

            foreach (var group in groupedProducts)
            {
                this.numberOfProductsInCategories.Add(group.Key, group.Count());
            }
        }

    }
}