using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teg.Com.Biz;
using Teg.Com.IBiz;
using Teg.Com.Utility;

namespace Teg.Com.Web.Controllers
{
    public class BaseController : Controller
    {
        public static IUserServices UserServices { get { return Utils.Singleton<UserServices>(); } }

        public static IRoomServices RoomServices { get { return Utils.Singleton<RoomServices>(); } }

        public static IBookingServices BookingServices { get { return Utils.Singleton<BookingServices>(); } }



        protected string GetAppSetting(Enum key)
        {
            var res = ConfigurationManager.AppSettings[key.ToString()];
            return res;
        }
    }
}