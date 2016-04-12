namespace LudogorieFood.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;
    using Infrastructure.Mapping;

    [Authorize(Roles = "Admin")]
    public class BaseAdminController : Controller
    {
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        public BaseAdminController()
        {
            
        }
    }
}