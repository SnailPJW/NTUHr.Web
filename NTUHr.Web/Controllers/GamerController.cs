using System.Web.Mvc;
using NTUHr.Web.Models;

namespace NTUHr.Web.Controllers
{
    public class GamerController : Controller
    {
        public ActionResult Details()
        {
            var gamer = new Gamer()
            {
                Id = 1,
                Name = "Name1",
                Gender = "Male",
                City = "City1"
            };
            return View(gamer);
        }
    }
}
