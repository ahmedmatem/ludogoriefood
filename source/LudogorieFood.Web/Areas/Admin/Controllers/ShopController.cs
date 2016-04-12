namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Common;
    using Models.Products;

    public class ShopController : BaseAdminController
    {
        private readonly IDbRepository<Category> categories;

        public ShopController(IDbRepository<Category> categories)
        {
            this.categories = categories;

            ViewData["ActiveMenuName"] = "Categories";
        }

        // GET: Admin/Shop
        public ActionResult Index()
        {
            return this.View();
        }
    }
}