
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teg.Com.Model;
using Teg.Com.Utility;
using Teg.Com.Web.Helper;
using Teg.Com.Web.Models;

namespace Teg.Com.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var vm = new BookingViewModel();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(BookingViewModel vm)
        {
            try
            {
                if (vm.RoomId > 0)
                {
                    #region Add booking
                    var booking = new Booking();
                    booking.Name = vm.Name;
                    booking.Company = vm.Company;
                    booking.Email = vm.Email;
                    booking.Phone = vm.Phone;
                    booking.From = vm.From;
                    booking.To = vm.To;
                    booking.Room = vm.RoomId;
                    booking.CreatedBy = "User";
                    booking.CreatedOn = DateTime.Now;
                    booking.UpdatedBy = "User";
                    booking.UpdatedOn = DateTime.Now;
                    BookingServices.Add(booking);
                    #endregion
                    #region Send Mail
                    var subject = "Booking Susscess";
                    string body = Utils.GetEmailTemplate("SendMail.html");
                    var listAttachment = new List<string>();
                    var formAddress = GetAppSetting(AppSettings.FromEmail);
                    Utils.SendEmailTest(subject, body, formAddress, "Admin", vm.Email, "User", formAddress, GetAppSetting(AppSettings.Password));
                    #endregion
                }
                else {
                    return View(vm);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return Content("<script language='javascript' type='text/javascript'>alert('Booking have been successfully');window.location.href = '" + @Url.Action("Index", "Home") + "';</script>");
        }
        [HttpPost]
        public ActionResult GetRoom()
        {
            try
            {
                //var param = Request.Form["query"];
                var listRooms = RoomServices.SearchFor(x => x.IsDelete == false);
                var listRoomVM = new List<RoomViewModel>();
                foreach (var item in listRooms)
                {
                    var room = new RoomViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Status = CheckStatus(item.Id)
                    };
                    listRoomVM.Add(room);
                }
                return Json(new { items = listRoomVM }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region helper
        public int CheckStatus(int id)
        {
            var booking = BookingServices.SearchFor(x => x.Room == id).ToList();
            if (booking.Count > 0)
            {
                if (booking.Where(x => x.From <= DateTime.Now && x.To >= DateTime.Now).ToList().Count > 0)
                {
                    //status provisional
                    return 2;
                }
            }
            else
            {
                var room = RoomServices.GetById(id);
                if (room.IsActive)
                {
                    // status available
                    return 1;
                }
                else
                {
                    //status invaiable
                    return 3;
                }
            }
            return 0;

        }
        #endregion
    }
}