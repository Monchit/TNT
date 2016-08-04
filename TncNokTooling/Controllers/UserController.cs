using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TncNokTooling.Controllers
{
    public class UserController : Controller
    {
        [Chk_Authen]
        public ActionResult Index()
        {
            return View();
        }

    }
}
