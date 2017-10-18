using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teg.Com.Admin.Models;

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
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
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

        [HttpPost]
        public ActionResult GetData()
        {
            var listData = new List<Example>
            {
                new Example
                {
                    CategoryId = 1,
                    CategoryName = "Name 1",
                    Description = "Desc 1",
                },
                new Example
                {
                    CategoryId = 2,
                    CategoryName = "Name 2",
                    Description = "Desc 1",
                },
                new Example
                {
                    CategoryId = 3,
                    CategoryName = "Name 3",
                    Description = "Desc 1",
                },
                new Example
                {
                    CategoryId = 4,
                    CategoryName = "Name 4",
                    Description = "Desc 1",
                },
                new Example
                {
                    CategoryId =5,
                    CategoryName = "Name 5",
                    Description = "Desc 1",
                }
            };

            return Json(new { ListData = listData }, JsonRequestBehavior.AllowGet);
        }
    }

    public class Example
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}