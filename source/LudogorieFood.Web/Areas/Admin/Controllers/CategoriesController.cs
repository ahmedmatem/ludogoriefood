namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Linq;

    using Common;
    using Models.Products;

    public class CategoriesController : BaseAdminController
    {
        IDbRepository<Category> categories;

        public CategoriesController(IDbRepository<Category> categories)
        {
            this.categories = categories;
        }

        // GET: Admin/Shop/Categories/
        public ActionResult Index()
        {
            TempData["ActiveMenuName"] = "Categories";

            var categories = this.categories.All().ToList();

            return this.View(categories);
        }
    }
}