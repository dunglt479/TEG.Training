using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Teg.Com.Model;

namespace Teg.Com.Web.Models
{
    public class BookingViewModel
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [Required(ErrorMessage = "Please Choose Room")]
        public int RoomId { get; set; }

        public IList<RoomViewModel> ListRoom { get; set; }
    }
}