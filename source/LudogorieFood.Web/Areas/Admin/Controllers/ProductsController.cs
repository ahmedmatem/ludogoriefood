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
        private readonly IDbRepository<Product> products;
        private readonly IDbRepository<Category> categories;

        public ProductsController(IDbRepository<Product> products, IDbRepository<Category> categories)
        {
            this.products = products;
            this.categories = categories;

            ViewData["ActiveMenuName"] = "Products";
        }

        // GET: Admin/Shop/Products
        public ActionResult Index()
        {
            var products = this.products
                .All()
                .To<ProductViewModel>()
                .OrderBy(p => p.CategoryId)
                .ThenBy(p => p.Name)
                .ToList();

            return this.View(products);
        }

        // GET: Admin/Shop/Products/Create
        [HttpGet]
        public ActionResult Create()
        {
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

        // POST: Admin/Shop/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var newProduct = this.Mapper.Map<Product>(model);

            if (model.Picture != null && model.Picture.ContentLength > 0)
            {                
                newProduct.PictureName = Helpers.CreateUniqueFileName(model.Picture.FileName) + "." + Helpers.GetFileTypeFromName(model.Picture.FileName).ConvertToPictureType();
                
                // store the product picture inside ~/Content/Images/Products folder
                var path = Path.Combine(Server.MapPath(GlobalConstants.ProductsImagesPath), newProduct.PictureName);
                model.Picture.SaveAs(path);
            }

            products.Add(newProduct);
            products.Save();

            return RedirectToAction("Index");
        }

        // GET: Admin/Shop/Products/Update/id
        public ActionResult Update(int id)
        {
            var product = this.products.GetById(id);
            var model = this.Mapper.Map<ProductViewModel>(product);

            var allCategories = this.categories.All().ToList();
            ViewBag.CategoryList = new List<SelectListItem>();
            foreach (var category in allCategories)
            {
                ViewBag.CategoryList.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Id.ToString(),
                    Selected = (category.Id == model.CategoryId) ? true : false,
                });
            }

            return this.View(model);
        }

        // POST: Admin/Shop/Products/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var productForUpdate = this.products.GetById(model.Id);
            
            productForUpdate.CategoryId = model.CategoryId;
            productForUpdate.Name = model.Name;
            productForUpdate.Measure = model.Measure;
            productForUpdate.Amount = model.Amount;
            productForUpdate.Price = model.Price;
            productForUpdate.NewPrice = model.NewPrice;

            if(model.Picture != null && model.Picture.ContentLength > 0 && model.Picture.FileName != GlobalConstants.DefaultNoPictureName)
                // there is new picture
            {
                string path;
                if (productForUpdate.PictureName != GlobalConstants.DefaultNoPictureName)
                    // there is old picture
                {
                    // delete old picture
                    path = Path.Combine(Server.MapPath(GlobalConstants.ProductsImagesPath), productForUpdate.PictureName);
                    if ((System.IO.File.Exists(path)))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                // and save new picture
                productForUpdate.PictureName = Helpers.CreateUniqueFileName(model.Picture.FileName) + "." + Helpers.GetFileTypeFromName(model.Picture.FileName).ConvertToPictureType();

                // store the product picture inside ~/Content/Images/Products folder
                path = Path.Combine(Server.MapPath(GlobalConstants.ProductsImagesPath), productForUpdate.PictureName);
                model.Picture.SaveAs(path);
            }

            productForUpdate.InStock = model.InStock;

            products.Update(productForUpdate);
            products.Save();

            return RedirectToAction("Index");
        }

        // GET: Admin/Shop/Products/Delete/id
        public ActionResult Delete(int id)
        {
            var productForDeleting = this.products.GetById(id);
            this.products.Delete(productForDeleting);
            this.products.Save();

            return RedirectToAction("Index");
        }
    }
}