using Microsoft.AspNetCore.Mvc;

namespace StudyRoomBooking.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}