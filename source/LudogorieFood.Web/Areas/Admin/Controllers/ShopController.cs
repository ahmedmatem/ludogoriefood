namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Common;
    using Models.Products;

    public class ShopController : BaseAdminController
    {
        private IDbRepository<Category> categories;

        public ShopController(IDbRepository<Category> categories)
        {
            this.categories = categories;
        }

        // GET: Admin/Shop
        public ActionResult Index()
        {
            return RedirectToAction("Categories");
        }

        // GET: Admin/Shop/Categories
        public ActionResult Categories()
        {
            TempData["ActiveMenuName"] = "Categories";

            var categories = this.categories.All().ToList();

            return this.View(categories);
        } 
    }
}