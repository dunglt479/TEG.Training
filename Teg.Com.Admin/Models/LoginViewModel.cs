using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Teg.Com.Admin.Models
{
    public class LoginViewModel
    {
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual bool RememberMe { get; set; }
    }
}