﻿using System.Web.Mvc;

namespace LudogorieFood.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Shop_products",
                "Admin/Shop/{controller}/{action}/{id}",
                new { controller = "Shop", action = "Index", id = UrlParameter.Optional},
                new string[] { "LudogorieFood.Web.Areas.Admin.Controllers"}
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "LudogorieFood.Web.Areas.Admin.Controllers" }
            );
        }
    }
}