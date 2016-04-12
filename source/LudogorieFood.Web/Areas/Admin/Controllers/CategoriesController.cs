namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Linq;

    using Common;
    using Models.Products;
    using Infrastructure.Mapping;
    using ViewModels.Shop;

    public class CategoriesController : BaseAdminController
    {
        private readonly IDbRepository<Category> categories;

        public CategoriesController(IDbRepository<Category> categories)
        {
            this.categories = categories;

            ViewData["ActiveMenuName"] = "Categories";
        }

        // GET: Admin/Shop/Categories/
        public ActionResult Index()
        {
            var categories = this.categories
                .All()
                .To<CategoryViewModel>()
                .ToList();

            return this.View(categories);
        }

        // GET: Admin/Shop/Categories/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Admin/Shop/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            
            var newCategory = this.Mapper.Map<Category>(model);
            categories.Add(newCategory);

            categories.Save();

            return RedirectToAction("Index");
        }

        // GET: Admin/Shop/Categories/Update/id
        [HttpGet]
        public ActionResult Update(int id)
        {
            var categoryForUpdate = this.categories.GetById(id);
            var model = this.Mapper.Map<CategoryViewModel>(categoryForUpdate);

            return this.View(model);
        }

        // POST: Admin/Shop/Categories/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var categoryForUpdate = this.categories.GetById(model.Id);
            categoryForUpdate.Name = model.Name;

            this.categories.Update(categoryForUpdate);
            this.categories.Save();

            return RedirectToAction("Index");
        }

        // GET: Admin/Shop/Categories/Delete/id
        public ActionResult Delete(int id)
        {
            var categoryForDeleting = this.categories.GetById(id);
            this.categories.Delete(categoryForDeleting);
            this.categories.Save();

            return RedirectToAction("Index");
        }
    }
}