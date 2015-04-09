using System.Web.Http;

using jcDDAL.lib;

namespace jcDDAL.Tests.WebAPI {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional},
                constraints: null,
                handler: new jcDDALDispatcher(config)
               );
        }
    }
}