namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public class BaseAdminController : Controller
    {
        public BaseAdminController()
        {
            
        }
    }
}