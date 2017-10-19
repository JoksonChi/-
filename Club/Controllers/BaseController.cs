using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Club.Controllers
{
    public class BaseController : Controller
    {
        public void ShowMsg(string Msg)
        {
            TempData["Msg"] = Msg;
        }
    }
}