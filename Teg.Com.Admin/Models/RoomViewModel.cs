﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teg.Com.Admin.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
    }
}