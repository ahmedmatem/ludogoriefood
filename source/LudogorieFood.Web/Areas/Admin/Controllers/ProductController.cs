namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Common;
    using Infrastructure.Mapping;

    using ViewModels.Shop;
    using Models.Products;

    public class ProductController : Controller
    {
        private IDbRepository<Product> products;

        public ProductController(IDbRepository<Product> products)
        {
            this.products = products;
        }

        // GET: Admin/Shop/Products
        public ActionResult Index()
        {
            TempData["ActiveMenuName"] = "Products";

            var products = this.products
                .All()
                .To<ProductViewModel>()
                .ToList();

            return this.View(products);
        }

        // GET: Admin/Shop/Products/Add
        [HttpGet]
        public ActionResult Add()
        {
            TempData["ActiveMenuName"] = "Products";

            return View();
        }
    }
}