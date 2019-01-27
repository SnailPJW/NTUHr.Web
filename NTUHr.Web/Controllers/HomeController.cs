using System.Collections.Generic;
using System.Web.Mvc;

namespace NTUHr.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ////1.
            ViewBag.Names = new List<string>
            {
                "ViewBag.Names01",
                "ViewBag.Names02",
                "ViewBag.Names03"
            };
            ////2.
            //ViewData["Names"] = new List<string>
            //{
            //    "ViewData[\"Names\"]01",
            //    "ViewData[\"Names\"]02",
            //    "ViewData[\"Names\"]03"
            //};
            ////3.
            //ViewBag.Names = new List<string>
            //{
            //    "ViewBag.Names01",
            //    "ViewBag.Names02",
            //    "ViewBag.Names03"
            //};
            //ViewData["Names"] = new List<string>
            //{
            //    "ViewData[\"Names\"]01",
            //    "ViewData[\"Names\"]02",
            //    "ViewData[\"Names\"]03"
            //};
            //4.
            //ViewBag.Names = new List<string>
            //{
            //    "ViewBag.Names01",
            //    "ViewBag.Names02",
            //    "ViewBag.Names03"
            //};
            //ViewData["Names2"] = new List<string>
            //{
            //    "ViewData[\"Names\"]01",
            //    "ViewData[\"Names\"]02",
            //    "ViewData[\"Names\"]03"
            //};
            return View();
        }
        //GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //public ActionResult Index()
        //{
        //    ViewBag.Names = new List<string>
        //        {
        //            "Name01",
        //            "Name02",
        //            "Name03"
        //        };
        //    return View();
        //}

        //public string Index(string id, string name)
        //{
        //    //string queryString = Request.QueryString["name"];
        //    //return $"Hello,{id}, name={queryString}";
        //    return $"Hello,{id}, name={name}";
        //}
        //public string GetStringA()
        //{
        //    return "Peggy";
        //}

    }
}