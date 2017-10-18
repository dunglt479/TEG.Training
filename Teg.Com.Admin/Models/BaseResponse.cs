using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teg.Com.Admin.Models
{
    public class BaseResponse
    {
        public string Msg { get; set; }

        public string RidirectUrl { get; set; }

        public BaseResponse()
        {
            Msg = Constants.MsgSuccess;
        }
    }

    public class Constants
    {
        public const string MsgSuccess = "Success";

        public const string MsgFail = "Fail";
    }
}