using System.Web.Mvc;

namespace NTUHr.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public string Index(string id, string name)
        {
            //string queryString = Request.QueryString["name"];
            //return $"Hello,{id}, name={queryString}";
            return $"Hello,{id}, name={name}";
        }
        public string GetStringA()
        {
            return "Peggy";
        }
    }
}