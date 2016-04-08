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
    using Infrastructure;
    using System.IO;

    public class ProductsController : BaseAdminController
    {
        private IDbRepository<Product> products;
        private IDbRepository<Category> categories;

        public ProductsController(IDbRepository<Product> products, IDbRepository<Category> categories)
        {
            this.products = products;
            this.categories = categories;
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
            var allCategories = this.categories.All().ToList();
            ViewBag.CategoryList = new List<SelectListItem>();
            foreach (var category in allCategories)
            {
                ViewBag.CategoryList.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Id.ToString(),
                });
            }

            return View();
        }

        // POST: Admin/Shop/Products/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if(model.Picture != null && model.Picture.ContentLength > 0)
            {
                var newProduct = new Product();
                newProduct.PictureName = Helpers.CreateUniqueFileName(model.Picture.FileName) + "." + Helpers.GetFileTypeFromName(model.Picture.FileName).ConvertToPictureType();
                newProduct.Price = model.Price;

                // store the product picture inside ~/Content/Images/Products folder
                var path = Path.Combine(Server.MapPath(GlobalConstants.ProductsImagesPath), newProduct.PictureName);
                model.Picture.SaveAs(path);

                products.Add(newProduct);
                products.Save();
            }

            return RedirectToAction("Index");
        }
    }
}