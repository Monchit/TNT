using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using TncNokTooling.Models;

namespace TncNokTooling.Controllers
{
    public class AdminController : Controller
    {
        private TncNokToolingEntities DBTNT = new TncNokToolingEntities();

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
                    //data.lv_min = 
                    //data.lv_max = 
                    //data.next = 

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
                //data.lv_min = 
                //data.lv_max = 
                //data.next = 
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

        public ActionResult Users()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = DBTNT.tm_user;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.emp_code,
                        s.utype_id
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

    }
}
