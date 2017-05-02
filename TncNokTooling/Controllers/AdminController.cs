using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TncNokTooling.Models;

namespace TncNokTooling.Controllers
{
    public class AdminController : Controller
    {
        private TncNokToolingEntities DBTNT = new TncNokToolingEntities();

        [Chk_Authen]
        public ActionResult Status()
        {
            return View();
        }

        [HttpPost]
        public JsonResult StatusList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_status;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.id,
                        s.name,
                        s.name2,
                        s.lv_min,
                        s.lv_max,
                        s.next
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateStatus()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var dbData = DBTNT.tm_status.Find(id);
                if (dbData == null)
                {
                    tm_status data = new tm_status();
                    data.id = id;
                    data.name = Request.Form["name"];
                    data.name2 = Request.Form["name2"];
                    data.lv_min = byte.Parse(Request.Form["lv_min"]);
                    data.lv_max = byte.Parse(Request.Form["lv_max"]);
                    data.next = byte.Parse(Request.Form["next"]);

                    DBTNT.tm_status.Add(data);
                }

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_status.OrderByDescending(o => o.id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateStatus()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_status.Find(id);
                data.name = Request.Form["name"];
                data.name2 = Request.Form["name2"];
                data.lv_min = byte.Parse(Request.Form["lv_min"]);
                data.lv_max = byte.Parse(Request.Form["lv_max"]);
                data.next = byte.Parse(Request.Form["next"]);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteStatus()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_status.Find(id);
                DBTNT.tm_status.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------------//

        [Chk_Authen]
        public ActionResult Action()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ActionList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_action;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.id,
                        s.name
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateAction()
        {
            try
            {
                var id = Request.Form["id"];
                var dbData = DBTNT.tm_action.Find(id);
                if (dbData == null)
                {
                    tm_action data = new tm_action();
                    data.id = id;
                    data.name = Request.Form["name"];

                    DBTNT.tm_action.Add(data);
                }

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_action.OrderByDescending(o => o.id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateAction()
        {
            try
            {
                var id = Request.Form["id"];
                var data = DBTNT.tm_action.Find(id);
                data.name = Request.Form["name"];
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteAction()
        {
            try
            {
                var id = Request.Form["id"];
                var data = DBTNT.tm_action.Find(id);
                DBTNT.tm_action.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------------//

        [Chk_Authen]
        public ActionResult UserType()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserTypeList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_user_type;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.id,
                        s.name
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateUserType()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var dbData = DBTNT.tm_user_type.Find(id);
                if (dbData == null)
                {
                    tm_user_type data = new tm_user_type();
                    data.id = id;
                    data.name = Request.Form["name"];

                    DBTNT.tm_user_type.Add(data);
                }

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_user_type.OrderByDescending(o => o.id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateUserType()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_user_type.Find(id);
                data.name = Request.Form["name"];
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteUserType()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_user_type.Find(id);
                DBTNT.tm_user_type.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------------//

        [Chk_Authen]
        public ActionResult Users()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.v_sys_user;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.emp_code,
                        s.emp_fname,
                        s.emp_lname,
                        s.name
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateUser()
        {
            try
            {
                var id = Request.Form["emp_code"];
                var dbData = DBTNT.tm_user.Find(id);
                if (dbData == null)
                {
                    tm_user data = new tm_user();
                    data.emp_code = id;
                    data.utype_id = byte.Parse(Request.Form["utype_id"]);

                    DBTNT.tm_user.Add(data);
                }

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_user.OrderByDescending(o => o.emp_code).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateUser()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_user.Find(id);
                data.utype_id = byte.Parse(Request.Form["utype_id"]);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteUser()
        {
            try
            {
                var id = Request.Form["id"];
                var data = DBTNT.tm_user.Find(id);
                DBTNT.tm_user.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------------//

        [Chk_Authen]
        public ActionResult UserNOK()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserNOKList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_user_nok;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.username,
                        s.password,//password = Decrypt(s.password),
                        s.fname,
                        s.lname,
                        s.utype,
                        s.email
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateUserNOK()
        {
            try
            {
                var id = Request.Form["username"];
                var dbData = DBTNT.tm_user_nok.Find(id);
                if (dbData == null)
                {
                    tm_user_nok data = new tm_user_nok();
                    data.username = id;
                    data.fname = Request.Form["fname"];
                    data.lname = Request.Form["lname"];
                    var keyNew = EncryptDecrypt.GeneratePassword(10);
                    data.password = EncryptDecrypt.EncodePassword(Request.Form["password"], keyNew);
                    data.vcode = keyNew;
                    data.email = Request.Form["email"];
                    //data.ulv = byte.Parse(Request.Form[""]);
                    data.utype = byte.Parse(Request.Form["utype"]);

                    DBTNT.tm_user_nok.Add(data);
                }

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_user_nok.OrderByDescending(o => o.username).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateUserNOK()
        {
            try
            {
                var id = Request.Form["username"];
                var data = DBTNT.tm_user_nok.Find(id);
                data.fname = Request.Form["fname"];
                data.lname = Request.Form["lname"];
                //data.password = Encode(Request.Form["password"]);
                data.email = Request.Form["email"];
                data.utype = byte.Parse(Request.Form["utype"]);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteUserNOK()
        {
            try
            {
                var id = Request.Form["username"];
                var data = DBTNT.tm_user_nok.Find(id);
                DBTNT.tm_user_nok.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetUtypeList()
        {
            try
            {
                var result = DBTNT.tm_user_type.Where(w => w.id > 20)
                    .Select(r => new { DisplayText = r.name, Value = r.id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------------//

        [Chk_Authen]
        public ActionResult FileType()
        {
            return View();
        }

        [HttpPost]
        public JsonResult FileTypeList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_file_type;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.id,
                        s.name
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateFileType()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var dbData = DBTNT.tm_file_type.Find(id);
                if (dbData == null)
                {
                    tm_file_type data = new tm_file_type();
                    data.id = id;
                    data.name = Request.Form["name"];

                    DBTNT.tm_file_type.Add(data);
                }

                DBTNT.SaveChanges();

                return Json(new { Result = "OK", Record = DBTNT.tm_file_type.OrderByDescending(o => o.id).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateFileType()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_file_type.Find(id);
                data.name = Request.Form["name"];
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteFileType()
        {
            try
            {
                var id = byte.Parse(Request.Form["id"]);
                var data = DBTNT.tm_file_type.Find(id);
                DBTNT.tm_file_type.Remove(data);
                DBTNT.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //--------------------------------------------//

        public ActionResult UpdatePO()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePOData(HttpPostedFileBase pofile)//
        {
            try
            {
                string pr = Request.Form["prno"];

                var query = DBTNT.td_pr.Find(pr);
                if (!string.IsNullOrEmpty(Request.Form["pono"])) query.po_no = Request.Form["pono"];

                //-------------------File Upload---------------------//

                string subPath = "~/upload/" + pr.Substring(3, 2) + "/" + pr + "/";
                if (!Directory.Exists(Server.MapPath(subPath)))
                    Directory.CreateDirectory(Server.MapPath(subPath));

                if (pofile != null && pofile.ContentLength > 0 && pofile.ContentType == "application/pdf")
                {
                    var fileName = Path.GetFileName(pofile.FileName.Replace('#', ' '));
                    var path = Path.Combine(Server.MapPath(subPath), fileName);
                    pofile.SaveAs(path);

                    if (DBTNT.td_files.Find(pr, fileName) == null)
                    {
                        td_files fdb = new td_files();
                        fdb.pr_no = pr;
                        fdb.file_name = fileName;
                        fdb.file_path = subPath;
                        fdb.file_type = 10;
                        fdb.upl_by = Session["TNT_Auth"].ToString();
                        fdb.upl_dt = DateTime.Now;
                        DBTNT.td_files.Add(fdb);
                    }
                }

                DBTNT.SaveChanges();

                TempData["noty_comp"] = "Update PO Completed.";
                return RedirectToAction("UpdatePO", "Admin");
            }
            catch (Exception ex)
            {
                TempData["noty_warn"] = "Error : " + ex;
                return RedirectToAction("UpdatePO", "Admin");
            }
        }

        [HttpGet]
        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult Selecte2PRNo(string searchTerm)
        {
            var sel = DBTNT.td_pr
                .Where(w => w.pr_no.Contains(searchTerm))
                .OrderBy(o => o.pr_no)
                .Select(s => new { id = s.pr_no, text = s.pr_no })
                .Take(16).ToList();
            return Json(sel, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public JsonResult GetPRPO(string id)
        {
            var data = (from a in DBTNT.td_pr
                        where a.pr_no == id
                        select new
                        {
                            a.pr_no,
                            a.po_no
                        }).FirstOrDefault();
            if (data != null)
                return Json(data, JsonRequestBehavior.AllowGet);
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        //--------------------------------------------//

        [Chk_Authen]
        public ActionResult ChangeNOKPassword()
        {
            ViewBag.NOKUser = DBTNT.tm_user_nok;
            return View();
        }

        [HttpPost]
        public ActionResult ChangeNOKPass()
        {
            var emp = Request.Form["seluser"];
            var get_user = DBTNT.tm_user_nok.Find(emp);
            if (get_user != null)
            {
                var keyNew = EncryptDecrypt.GeneratePassword(10);
                get_user.password = EncryptDecrypt.EncodePassword(Request.Form["pass_confirmation"], keyNew);
                get_user.vcode = keyNew;
                DBTNT.SaveChanges();
                //Change password success
            }

            return RedirectToAction("ChangeNOKPassword", "Admin");
        }
    }
}
