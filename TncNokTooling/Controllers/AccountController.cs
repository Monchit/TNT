using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommonFunction;
using TncNokTooling.Models;

namespace TncNokTooling.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private TNCSecurity secure = new TNCSecurity();
        private TNC_ADMINEntities dbTNC = new TNC_ADMINEntities();
        private TncNokToolingEntities DBTNT = new TncNokToolingEntities();

        //[HttpGet]
        public ActionResult Index(string key = null)
        {
            if (key != null)
            {
                return Login(key);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Index1(string key = null)
        {
            if (key != null)
            {
                return Login(key);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string key = null)
        {
            string username = key == null ? Request.Form["username"].ToString() : "";
            string password = key == null ? Request.Form["password"].ToString() : "";

            var chklogin = secure.Login(username, password, true);//set false to true for Real

            if (key != null)
            {
                username = secure.WebCenterDecode(key);
                chklogin = secure.Login(username, "a", false);
            }

            if (chklogin != null)
            {
                Session["TNT_Auth"] = chklogin.emp_code;

                var ulv = (from a in dbTNC.tnc_user_level
                           where a.position_min <= chklogin.position_level && a.position_max >= chklogin.position_level
                           select a).FirstOrDefault();

                if (ulv != null)
                {
                    Session["TNT_ULv"] = ulv.ulv_id;
                    Session["TNT_Org"] = chklogin.LeafOrgGroupId;

                    if (chklogin.emp_lname.Length > 1)
                    {
                        Session["TNT_Name"] = chklogin.emp_fname + " " + chklogin.emp_lname.Substring(0, 1) + ".";
                    }
                    else
                    {
                        Session["TNT_Name"] = chklogin.emp_fname + " " + chklogin.emp_lname;
                    }
                }

                var chk_sys_user = (from a in DBTNT.tm_user
                                    where a.emp_code == chklogin.emp_code
                                    select a).FirstOrDefault();

                Session["TNT_UType"] = chk_sys_user != null ? chk_sys_user.tm_user_type.id : 0;

                TempData["noty_comp"] = "Welcome to TNC/NOK Tooling System !!!";

                if (Session["TNT_Redirect"] != null)
                {
                    string url = Session["TNT_Redirect"].ToString();
                    Session.Remove("TNT_Redirect");
                    return Redirect(url);
                }
                else
                {
                    return RedirectToAction("TNCSearch", "Home");
                }
            }
            else
            {
                //var nok_user = 
                //if(){
                //return RedirectToAction("NOKSearch", "Home");
                //}else{
                    TempData["noty_warn"] = "Username and/or Password is incorrect !!!";
                    return RedirectToAction("Index", "Account");
                //}
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("TNT_Auth");
            Session.Remove("TNT_Name");
            Session.Remove("TNT_UType");
            Session.Remove("TNT_ULv");
            Session.Remove("TNT_Org");
            TempData["noty_comp"] = "You've successfully logged out...";
            return RedirectToAction("Index", "Account");
        }
    }
}