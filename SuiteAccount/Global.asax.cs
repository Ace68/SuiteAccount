using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApi.Hal;

namespace SuiteAccount
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectHttpContainer.RegisterModules();

            // default media type (HAL JSON)
            GlobalConfiguration.Configuration.Formatters.Insert(
                0, new JsonHalMediaTypeFormatter()
            );

            // alternative media type (HAL XML)
            // accept header must equal application/hal+xml
            GlobalConfiguration.Configuration.Formatters.Insert(
                1, new XmlHalMediaTypeFormatter()
            );
        }
    }
}
