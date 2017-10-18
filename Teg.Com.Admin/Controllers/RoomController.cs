using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Teg.Com.Admin.Models;
using Teg.Com.Model;

namespace Teg.Com.Admin.Controllers
{
    public class RoomController : BaseController
    {
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRoom()
        {
            //var param = Request.Form["query"];
            var listRooms = RoomServices.SearchFor(x => x.IsDelete == false);
            var listRoomVm = new List<RoomViewModel>();
            foreach (var item in listRooms)
            {
                var room = new RoomViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Status = CheckStatus(item.Id)
                };
                listRoomVm.Add(room);
            }
            return Json(new { ListData = listRoomVm }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(RoomViewModel model)
        {
            var res = new BaseResponse();
            if (string.IsNullOrEmpty(model?.Name) || string.IsNullOrEmpty(model.Description))
            {
                res.Msg = Constants.MsgFail;
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var room = new Room
                {
                    Name = model.Name,
                    Description = model.Description,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now,
                    UpdatedBy = "Admin",
                    UpdatedOn = DateTime.Now
                };

                RoomServices.Add(room);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                res.Msg = Constants.MsgFail;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Edit(RoomViewModel model)
        {
            var res = new BaseResponse();
            if (string.IsNullOrEmpty(model?.Name) || string.IsNullOrEmpty(model.Description))
            {
                res.Msg = Constants.MsgFail;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var room = RoomServices.GetById(model.Id);
                if (room != null)
                {
                    room.Name = model.Name;
                    room.Description = model.Description;
                    room.UpdatedOn = DateTime.Now;
                    room.UpdatedBy = "Admin";

                    RoomServices.Update(room);
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                res.Msg = "Cannot edit this item!";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                res.Msg = "Cannot edit this item!";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var res = new BaseResponse();
            if (id == 0)
            {
                res.Msg = Constants.MsgFail;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var room = RoomServices.GetById(id);
                RoomServices.Delete(room);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                res.Msg = "Cannot delete this item!";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        private int CheckStatus(int id)
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
                return room.IsActive ? 1 : 3;
                //status invaiable
            }
            return 0;

        }
    }
}