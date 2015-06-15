using System.Web.Mvc;

namespace SuiteAccount.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "SuiteAccount";

            return View();
        }
    }
}
