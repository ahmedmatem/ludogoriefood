namespace LudogorieFood.Web
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class HtmlHelpers
    {
        public static string IsActive(this HtmlHelper html, string controller = "", string action = "", string cssClass = "active")
        {
            ViewContext viewContext = html.ViewContext;
            bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = html.ViewContext.ParentActionViewContext;

            RouteValueDictionary routeValues = viewContext.RouteData.Values;
            string currentAction = routeValues["action"].ToString().ToLower();
            string currentController = routeValues["controller"].ToString().ToLower();

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            return action.ToLower().Contains(currentAction) && controller.ToLower().Contains(currentController) ?
                cssClass : String.Empty;
        }
    }
}
