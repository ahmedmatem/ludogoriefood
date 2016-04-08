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
            TempData["ActiveMenuName"] = "Categories";

            return this.View();
        }
    }
}