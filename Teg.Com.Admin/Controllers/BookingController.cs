using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teg.Com.Admin.Controllers
{
    public class BookingController : BaseController
    {
        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }
    }
}