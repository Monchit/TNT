using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using TncNokTooling.Models;
using WebCommonFunction;

namespace TncNokTooling.Controllers
{
    public class HomeController : Controller
    {
        private TncNokToolingEntities TntDb = new TncNokToolingEntities();
        private TNC_ADMINEntities dbTNC = new TNC_ADMINEntities();

        [Chk_Authen]
        public ActionResult Index()
        {
            ViewBag.NOKPlant = from a in TntDb.tm_nok_plant
                               select a;
            ViewBag.Process = from a in TntDb.tm_process
                              select a;
            return View();
        }

        //[Chk_Authen]
        //public ActionResult IssuePR()
        //{
        //    if (Session["TNT_ULv"].ToString() != "1")
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    var emp = Session["TNT_Auth"].ToString();
        //    ViewBag.GroupName = dbTNC.V_Employee_Info.Where(w => w.emp_code == emp).Select(s => s.group_name).FirstOrDefault();
        //    ViewBag.NOKPlant = from a in TntDb.tm_nok_plant
        //                       select a;
        //    return View();
        //}

        [Chk_Authen]
        public ActionResult Issue()
        {
            //if (Session["TNT_ULv"].ToString() != "1")
            //{
            //    return RedirectToAction("Index");
            //}
            var emp = Session["TNT_Auth"].ToString();
            ViewBag.GroupName = dbTNC.V_Employee_Info.Where(w => w.emp_code == emp).FirstOrDefault();
            ViewBag.NOKPlantList = TntDb.tm_nok_plant;
            ViewBag.ProcessList = TntDb.tm_process;
            ViewBag.ProductList = TntDb.tm_product;
            ViewBag.TypeList = TntDb.tm_type;
            ViewBag.CostCode = dbTNC.View_Organization.Where(w => w.active == true).OrderBy(o => o.cost_code);

            return View();
        }

        [Chk_Authen]
        public ActionResult EditPR(string pr_no, byte status, byte ulv, int org, byte rev)
        {
            var query = TntDb.td_pr.Find(pr_no);
            var emp = Session["TNT_Auth"].ToString();
            ViewBag.GroupName = dbTNC.V_Employee_Info.Where(w => w.emp_code == emp).FirstOrDefault();
            ViewBag.NOKPlantList = TntDb.tm_nok_plant;
            ViewBag.ProcessList = TntDb.tm_process;
            ViewBag.ProductList = TntDb.tm_product;
            ViewBag.TypeList = TntDb.tm_type;
            ViewBag.CostCode = dbTNC.View_Organization.Where(w => w.active == true).OrderBy(o => o.cost_code);
            ViewBag.Tran = TntDb.td_tran.Find(pr_no, status, ulv, org, rev);
            return View(query);
        }

        //-----------------------------------------//

        public JsonResult GetDescriptionByPlant(string plant)
        {
            if (string.IsNullOrEmpty(plant))
            {
                throw new ArgumentNullException("plant");
            }
            byte pid = 0;
            bool isValid = Byte.TryParse(plant, out pid);

            var result = (from a in TntDb.tm_leadtime
                          where a.plant == pid
                          select new
                          {
                              Value = a.tm_process.id,
                              Text = a.tm_process.name
                          }).Distinct();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductByDescription(string plant, string desc)
        {
            if (string.IsNullOrEmpty(plant))
            {
                throw new ArgumentNullException("plant");
            }
            else if (string.IsNullOrEmpty(desc))
            {
                throw new ArgumentNullException("description");
            }
            //byte sid = 0;
            //bool isValid = Byte.TryParse(plant, out sid);
            byte pid = Byte.Parse(plant);
            byte did = Byte.Parse(desc);

            var result = (from a in TntDb.tm_leadtime
                          where a.plant == pid && a.process == did
                          select new
                          {
                              Value = a.tm_product.id,
                              Text = a.tm_product.name
                          }).Distinct();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTypeByProduct(string plant, string desc, string prod)
        {
            if (string.IsNullOrEmpty(plant))
            {
                throw new ArgumentNullException("plant");
            }
            else if (string.IsNullOrEmpty(desc))
            {
                throw new ArgumentNullException("description");
            }
            else if (string.IsNullOrEmpty(prod))
            {
                throw new ArgumentNullException("product");
            }
            //byte sid = 0;
            //bool isValid = Byte.TryParse(id, out sid);
            byte pid = Byte.Parse(plant);
            byte did = Byte.Parse(desc);
            byte pdid = Byte.Parse(prod);

            var result = (from a in TntDb.tm_leadtime
                          where a.plant == pid && a.process == did && a.product == pdid
                          select new
                          {
                              Value = a.tm_type.id,
                              Text = a.tm_type.name
                          }).Distinct();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //-----------------------------------------//

        [HttpPost]
        public JsonResult PRList(string id, string po_no, string item, byte status = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = from a in TntDb.v_tran
                            where a.status_id < 90
                            select a;

                if (!string.IsNullOrEmpty(id))
                {
                    query = query.Where(w => w.pr_no.Contains(id));
                }
                //if (!string.IsNullOrEmpty(po_no))
                //{
                //    query = query.Where(w => w.po.Contains(po_no));
                //}
                if (!string.IsNullOrEmpty(item))
                {
                    query = query.Where(w => w.item_code.Contains(item));
                }
                if (status != 0)
                {
                    query = query.Where(w => w.status_id == status);
                }

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.pr_no,
                        s.item_code,
                        s.condition,
                        s.rank,
                        s.issue_dt,
                        s.due_date,
                        s.act_dt,
                        s.status,
                        s.status_id,
                        //s.ulv_id,
                        s.group_name
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public JsonResult ToolingList(string pr_no, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        var query = from a in TntDb.td_tooling
        //                    where a.pr_no == pr_no
        //                    select a;

        //        int TotalRecord = query.Count();

        //        var output = query
        //            .Select(s => new
        //            {
        //                s.pr_no,
        //                s.pr_line,
        //                s.description,
        //                s.qty,
        //                s.unit,
        //                s.sell
        //            }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

        //        return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        [HttpPost]
        public ActionResult CreatePR(string[] hcDesc, int[] hcQty, string[] hcUnit, bool[] hcSell, IEnumerable<HttpPostedFileBase> atfiles
            , HttpPostedFileBase fmail, HttpPostedFileBase fwr, HttpPostedFileBase fmom, HttpPostedFileBase fdraw)
        {
            try
            {
                TNCRunNumber run = new TNCRunNumber();
                TNCConversion convert = new TNCConversion();
                //TNCOrganization tnc_org = new TNCOrganization();

                int org = int.Parse(Session["TNT_Org"].ToString());

                //---------------Add PR----------------//
                var runno = run.GetRunNumber(23);
                td_pr pr = new td_pr();
                pr.pr_no = runno;
                var condition = Request.Form["condition"];
                pr.condition = condition;
                pr.due_date = convert.DateDisplayToDB(Request.Form["due_dt"]);
                if (condition == "Urgent") pr.reason = Request.Form["reason"];
                pr.issue_dt = DateTime.Now;
                string issuer = Session["TNT_Auth"].ToString();
                pr.issue_by = issuer;
                pr.issue_group = org;
                if (Request.Form["issue_dept"] != "")
                {
                    pr.issue_dept = int.Parse(Request.Form["issue_dept"]);
                }
                if (Request.Form["issue_plant"] != "")
                {
                    pr.issue_plant = int.Parse(Request.Form["issue_plant"]);
                }
                pr.ext = Request.Form["ext"];
                pr.rank = Request.Form["rank"];
                pr.cost_code = Request.Form["cost_code"];
                pr.item_code = Request.Form["item_code"];
                byte process = byte.Parse(Request.Form["process"]);
                pr.process = process;
                pr.nok_plant = byte.Parse(Request.Form["nok_plant"]);
                pr.product = byte.Parse(Request.Form["product"]);
                pr.type = byte.Parse(Request.Form["type"]);
                if (process == 1)
                {
                    pr.problem = Request.Form["problem"];
                    pr.nok_contact = Request.Form["contact"];
                }
                pr.issue_name = Request.Form["issue_by"];
                pr.issue_group_name = Request.Form["group_name"];

                TntDb.td_pr.Add(pr);

                //---------------Add Tooling----------------//

                for (short i = 0; i < hcDesc.Length; i++)
                {
                    td_tooling tool = new td_tooling();
                    tool.pr_no = runno;
                    tool.pr_line = (short)(i + 1);
                    tool.description = hcDesc[i];
                    tool.qty = hcQty[i];
                    tool.unit = hcUnit[i];
                    tool.sell = hcSell[i];

                    TntDb.td_tooling.Add(tool);
                }

                //---------------Add Files----------------//

                if (Request.Form["process"] == "1")
                {
                    AddFile(fmail, runno, 1);
                    AddFile(fwr, runno, 2);
                    AddFile(fmom, runno, 3);
                    AddFile(fdraw, runno, 4);
                }

                foreach (var atfile in atfiles)
                {
                    AddFile(atfile, runno, 9);
                }

                //---------------Add Transaction----------------//

                byte ulv = byte.Parse(Session["TNT_ULv"].ToString());

                ManageTran(runno, 1, ulv, org, 0, issuer, "ISS");

                TntDb.SaveChanges();
                run.SetRunNumber(23);

                return RedirectToAction("Index", "Home");
            }
            //catch (InvalidOperationException opex)
            //{
            //    TempData["noty_warn"] = "Invalid Operation -> " + opex;
            //    return RedirectToAction("Issue", "Home");
            //}
            catch (Exception ex)
            {
                TempData["noty_warn"] = "Error on Create PR -> " + ex;
                return RedirectToAction("Issue", "Home");
            }
        }

        [HttpPost]
        public ActionResult UpdatePR(string[] hcDesc, int[] hcQty, string[] hcUnit, bool[] hcSell, IEnumerable<HttpPostedFileBase> atfiles
            , HttpPostedFileBase fmail, HttpPostedFileBase fwr, HttpPostedFileBase fmom, HttpPostedFileBase fdraw)
        {
            var pr_no = Request.Form["pr_no"];
            try
            {
                TNCConversion convert = new TNCConversion();
                //TNCOrganization tnc_org = new TNCOrganization();

                int org = int.Parse(Session["TNT_Org"].ToString());

                //---------------Update PR----------------//
                var pr = TntDb.td_pr.Find(pr_no);
                var condition = Request.Form["condition"];
                pr.condition = condition;
                pr.due_date = convert.DateDisplayToDB(Request.Form["due_dt"]);
                if (condition == "Urgent") pr.reason = Request.Form["reason"];
                else pr.reason = null;
                //pr.issue_dt = DateTime.Now;
                string issuer = Session["TNT_Auth"].ToString();
                //pr.issue_by = issuer;
                //pr.issue_group = org;
                if (Request.Form["issue_dept"] != "")
                {
                    pr.issue_dept = int.Parse(Request.Form["issue_dept"]);
                }
                if (Request.Form["issue_plant"] != "")
                {
                    pr.issue_plant = int.Parse(Request.Form["issue_plant"]);
                }
                pr.ext = Request.Form["ext"];
                pr.rank = Request.Form["rank"];
                pr.cost_code = Request.Form["cost_code"];
                pr.item_code = Request.Form["item_code"];
                byte process = byte.Parse(Request.Form["hprocess"]);
                //pr.process = process;
                pr.nok_plant = byte.Parse(Request.Form["nok_plant"]);
                pr.product = byte.Parse(Request.Form["product"]);
                pr.type = byte.Parse(Request.Form["type"]);
                if (process == 1)
                {
                    pr.problem = Request.Form["problem"];
                    pr.nok_contact = Request.Form["contact"];
                }
                else
                {
                    pr.problem = null;
                    pr.nok_contact = null;
                }
                //pr.issue_name = Request.Form["issue_by"];

                //---------------Remove Tooling----------------//

                var currtool = TntDb.td_tooling.Where(w => w.pr_no == pr_no);
                foreach (var item in currtool)
                {
                    var deltool = TntDb.td_tooling.Find(item.pr_no, item.pr_line);
                    TntDb.td_tooling.Remove(deltool);
                }

                //---------------Add Tooling----------------//

                for (short i = 0; i < hcDesc.Length; i++)
                {
                    td_tooling tool = new td_tooling();
                    tool.pr_no = pr_no;
                    tool.pr_line = (short)(i + 1);
                    tool.description = hcDesc[i];
                    tool.qty = hcQty[i];
                    tool.unit = hcUnit[i];
                    tool.sell = hcSell[i];

                    TntDb.td_tooling.Add(tool);
                }

                ////---------------Add Files----------------//

                //if (Request.Form["process"] == "1")
                //{
                //    AddFile(fmail, pr_no, 1);
                //    AddFile(fwr, pr_no, 2);
                //    AddFile(fmom, pr_no, 3);
                //    AddFile(fdraw, pr_no, 4);
                //}

                foreach (var atfile in atfiles)
                {
                    AddFile(atfile, pr_no, 9);
                }

                //---------------Add Transaction----------------//

                byte ulv = byte.Parse(Session["TNT_ULv"].ToString());

                int torg = int.Parse(Request.Form["hdorg"]);
                byte tulv = byte.Parse(Request.Form["hdulv"]);
                //byte status = byte.Parse(Request.Form["hdstt"]);
                byte rev = byte.Parse(Request.Form["hdrev"]);

                UpdateTran(pr_no, 1, tulv, torg, rev, issuer, "EDIT", "Data Updated.");
                ManageTran(pr_no, 1, ulv, org, rev, issuer, "EDIT");

                TntDb.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            //catch (InvalidOperationException opex)
            //{
            //    TempData["noty_warn"] = "Invalid Operation -> " + opex;
            //    return RedirectToAction("Issue", "Home");
            //}
            catch (Exception ex)
            {
                TempData["noty_warn"] = "Error on Update PR -> " + ex;
                return RedirectToAction("EditPR", "Home", new { id = pr_no });
            }
        }

        private void ManageTran(string pr_no, byte status, byte ulv, int org, byte rev, string actor, string action, string content = null)
        {
            TNCOrganization get_org = new TNCOrganization();
            var chk_status = TntDb.tm_status.Find(status);
            if (action == "APV")
            {
                if (ulv < chk_status.lv_max)//Current Status
                {
                    get_org.GetApprover(actor);
                    CreateTran(pr_no, status, (byte)(get_org.OrgLevel + 1), get_org.OrgId, rev, get_org.ManagerId);
                    SendEmailCenter(pr_no, get_org.ManagerEMail, status: chk_status.name);
                }
                else //Next Status
                {
                    if (status == 1)//Issuer
                    {
                        var get_cost = TntDb.td_pr.Find(pr_no);
                        var get_owner = (from a in dbTNC.View_Organization
                                         where a.cost_code == get_cost.cost_code
                                         select a).FirstOrDefault();

                        if (get_owner != null)
                        {
                            if (!string.IsNullOrEmpty(get_owner.GroupMgr))
                            {
                                CreateTran(pr_no, chk_status.next.Value, 2, get_owner.group_id.Value, rev, get_owner.GroupMgr);
                                SendEmailCenter(pr_no, get_owner.GroupMgr_email, status: GetStatusName(chk_status.next.Value));
                            }
                            else if (!string.IsNullOrEmpty(get_owner.DeptMgr))
                            {
                                CreateTran(pr_no, chk_status.next.Value, 3, get_owner.dept_id.Value, rev, get_owner.DeptMgr);
                                SendEmailCenter(pr_no, get_owner.DeptMgr_email, status: GetStatusName(chk_status.next.Value));
                            }
                            else if (!string.IsNullOrEmpty(get_owner.PlantMgr))
                            {
                                CreateTran(pr_no, chk_status.next.Value, 4, get_owner.plant_id.Value, rev, get_owner.PlantMgr);
                                SendEmailCenter(pr_no, get_owner.PlantMgr_email, status: GetStatusName(chk_status.next.Value));
                            }
                        }
                    }
                    else if (status == 2 || status == 3)// goto Import-Export Confirm PR
                    {
                        UpdateDue(pr_no);
                        CreateTran(pr_no, chk_status.next.Value, 1, 133, rev);//133=Import-Export
                        SendEmailCenter(pr_no, getMailImEx(), status: GetStatusName(chk_status.next.Value));
                    }
                    else if (status == 4)// goto Issue PO in NBCS
                    {
                        CreateTran(pr_no, chk_status.next.Value, 1, 0, rev);
                        SendEmailCenter(pr_no, getMailImEx(), status: GetStatusName(chk_status.next.Value));
                    }
                    else if (status == 5)// goto HQ
                    {
                        CreateTran(pr_no, chk_status.next.Value, 1, 0, rev);
                        //SendEmailCenter(pr_no, getMailHQ(), status: TntDb.tm_status.Find(chk_status.next.Value).name);
                    }
                }
            }
            else if (action == "ISS")
            {
                CreateTran(pr_no, status, ulv, org, rev, actor, action);
                get_org.GetApprover(actor);

                var sp_route = TntDb.tm_control_route.Find(org);
                if (sp_route != null)//Short Route
                {
                    CreateTran(pr_no, 3, (byte)(get_org.OrgLevel + 1), get_org.OrgId, 0, get_org.ManagerId);
                    SendEmailCenter(pr_no, get_org.ManagerEMail, status: GetStatusName(3));
                }
                else//Normal Route
                {
                    CreateTran(pr_no, status, (byte)(get_org.OrgLevel + 1), get_org.OrgId, 0, get_org.ManagerId);
                    SendEmailCenter(pr_no, get_org.ManagerEMail, status: chk_status.name);
                }
            }
            else if (action == "EDIT")
            {
                var sp_route = TntDb.tm_control_route.Find(org);
                get_org.GetApprover(actor);
                if (sp_route != null)//Short Route
                {
                    CreateTran(pr_no, 3, (byte)(get_org.OrgLevel + 1), get_org.OrgId, rev, get_org.ManagerId);
                    SendEmailCenter(pr_no, get_org.ManagerEMail, status: GetStatusName(3));
                }
                else//Normal Route
                {
                    CreateTran(pr_no, status, (byte)(get_org.OrgLevel + 1), get_org.OrgId, rev, get_org.ManagerId);
                    SendEmailCenter(pr_no, get_org.ManagerEMail, status: chk_status.name);
                }
            }
            else if (action == "REJ")
            {
                var get_iss_tran = (from a in TntDb.td_tran
                                    where a.act_id == "ISS" && a.pr_no == pr_no
                                    select a).FirstOrDefault();
                if (get_iss_tran != null)
                {
                    CreateTran(pr_no, 1, get_iss_tran.ulv_id, get_iss_tran.org, (byte)(rev + 1), get_iss_tran.actor, content: "Update PR");
                    SendEmailCenter(pr_no, GetEmailbyEmp(get_iss_tran.actor), 1, content);
                }
            }
            else if (action == "CANC")
            {
                CreateTran(pr_no, 98, ulv, org, rev, actor);
                //SendEmailCenter(pr_no, );
            }

            //else if (action == "REV")
            //{
            //    get_org.GetApprover(actor);

            //    var sp_route = TntDb.tm_control_route.Find(org);
            //    if (sp_route != null)//Short Route
            //    {
            //        CreateTran(pr_no, 3, (byte)(get_org.OrgLevel + 1), get_org.OrgId, (byte)(rev + 1), get_org.ManagerId);
            //        //SendEmailCenter(pr_no, get_org.ManagerEMail);
            //    }
            //    else//Normal Route
            //    {
            //        CreateTran(pr_no, status, (byte)(get_org.OrgLevel + 1), get_org.OrgId, (byte)(rev + 1), get_org.ManagerId);
            //        //SendEmailCenter(pr_no, get_org.ManagerEMail);
            //    }
            //}
        }

        private string GetStatusName(byte id)
        {
            var query = TntDb.tm_status.Find(id);
            return query != null ? query.name + " " + query.name2 : "";
        }

        private string GetEmailbyEmp(string emp_code)
        {
            var query = (from a in dbTNC.tnc_user
                         where a.emp_code == emp_code && !(a.email == null || a.email.Equals("")) && a.emp_status == "A"
                         select a).FirstOrDefault();
            return query != null ? query.email : "";
        }

        private string getMailImEx()
        {
            var query = from a in TntDb.tm_user
                        where a.utype_id > 13 && a.utype_id < 20
                        select a.emp_code;

            string email = "";

            foreach (var item in query)
            {
                var getmail = (from a in dbTNC.tnc_user
                               where a.emp_code == item && (a.email != null && a.email != "")
                               select a).FirstOrDefault();

                if (getmail != null) email += ", " + getmail.email;
            }
            email = email.Substring(2);

            return email;
        }

        private void UpdateDue(string pr_no)
        {
            var query = TntDb.td_pr.Find(pr_no);
            if (query != null && query.condition == "Normal")
            {
                var lt = TntDb.tm_leadtime.Find(query.process, query.nok_plant, query.product, query.type);
                if (lt != null)
                {
                    int cal = lt.etd + lt.inspection + lt.making + lt.trial + lt.shipping + lt.eta;
                    query.due_date = DateTime.Now.AddDays(cal * 7);
                }
                else
                {
                    query.due_date = DateTime.Now.AddDays(15 * 7);//15 Weeks
                }
            }
        }

        private void CreateTran(string pr_no, byte status, byte ulv, int org, byte rev, string actor = null, string action = null, string content = null)
        {
            if (TntDb.td_tran.Find(pr_no, status, ulv, org, rev) == null)
            {
                td_tran tr = new td_tran();
                tr.pr_no = pr_no;
                tr.status_id = status;
                tr.ulv_id = ulv;
                tr.org = org;
                tr.rev = rev;
                tr.actor = actor;
                tr.act_id = action;
                tr.act_dt = DateTime.Now;
                if (!string.IsNullOrEmpty(content)) tr.comment = content;
                TntDb.td_tran.Add(tr);
            }
        }

        private void UpdateTran(string pr_no, byte status, byte ulv, int org, byte rev, string actor = null, string action = null, string content = null)
        {
            var tr = TntDb.td_tran.Find(pr_no, status, ulv, org, rev);
            if (tr != null)
            {
                tr.actor = actor;
                tr.act_id = action;
                tr.act_dt = DateTime.Now;
                if(!string.IsNullOrEmpty(content)) tr.comment = content;
            }
        }

        [HttpGet]
        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public string GetLeadTime(byte process, byte plant, byte product, byte type)
        {
            var lt = (from a in TntDb.tm_leadtime
                      where a.process == process && a.plant == plant && a.product == product && a.type == type
                      select a).FirstOrDefault();

            if (lt != null)
            {
                int cal = lt.etd + lt.inspection + lt.making + lt.trial + lt.shipping + lt.eta;
                return "+" + cal + "w";
            }
            else
            {
                return "+15w";
            }
        }

        protected void AddFile(HttpPostedFileBase file, string pr_no, byte file_type)
        {
            string subPath = "~/upload/" + pr_no.Substring(3, 2) + "/" + pr_no + "/";
            if (!Directory.Exists(Server.MapPath(subPath)))
                Directory.CreateDirectory(Server.MapPath(subPath));

            if (file != null && file.ContentLength > 0 && file.ContentType == "application/pdf")
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath(subPath), fileName);
                file.SaveAs(path);

                if (TntDb.td_files.Find(pr_no, fileName) == null)
                {
                    td_files fdb = new td_files();
                    fdb.pr_no = pr_no;
                    fdb.file_name = fileName;
                    fdb.file_path = subPath;
                    fdb.file_type = file_type;
                    fdb.upl_by = Session["TNT_Auth"].ToString();
                    fdb.upl_dt = DateTime.Now;
                    TntDb.td_files.Add(fdb);
                }
            }
        }

        protected void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists) file.Delete();
        }

        [HttpPost]
        public JsonResult GetUserLvList()
        {
            try
            {
                var result = dbTNC.tnc_user_level
                    .Select(r => new { DisplayText = r.ulv_detail, Value = r.ulv_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpGet]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _TabPR(string pr_no)
        {
            var query = (from a in TntDb.td_pr
                         where a.pr_no == pr_no
                         select a).FirstOrDefault();

            ViewBag.Tooling = TntDb.td_tooling.Where(w => w.pr_no == pr_no).OrderBy(o => o.pr_line);

            return PartialView(query);
        }

        [HttpGet]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _TabApprove(string pr_no)
        {
            var query = from a in TntDb.td_tran
                        where a.pr_no == pr_no && a.act_id == null
                        select a;

            byte ulv = byte.Parse(Session["TNT_ULv"].ToString());
            int org = int.Parse(Session["TNT_Org"].ToString());
            string actor = Session["TNT_Auth"].ToString();

            if (query.Any())
            {
                foreach (var item in query)
                {
                    if (item.status_id == 4)
                    {
                        if (TntDb.tm_user.Any(w => w.emp_code == actor && w.utype_id > 10 && w.utype_id < 20))
                        {
                            ViewBag.CurrentTran = item;
                        }
                    }
                    else if (item.ulv_id == ulv && item.org == org)//Direct
                    {
                        ViewBag.CurrentTran = item;
                    }
                    else if (item.ulv_id < ulv)//Instead
                    {
                        if (ulv == 2 && item.org == org)//Mgr. instead of Eng.
                        {
                            ViewBag.CurrentTran = item;
                        }
                        else if (item.ulv_id == 2)//Wait Mgr.
                        {
                            var get_org = (from a in dbTNC.View_Organization
                                           where a.group_id == item.org
                                           select a).FirstOrDefault();
                            if (get_org != null)
                            {
                                if (ulv == 3 && get_org.dept_id == org)//Dept. instead of Mgr.
                                {
                                    ViewBag.CurrentTran = item;
                                }
                                else if (ulv == 4 && get_org.plant_id == org)//Plant instead of Mgr.
                                {
                                    ViewBag.CurrentTran = item;
                                }
                            }
                        }
                        else if (item.ulv_id == 3)//Wait Dept.
                        {
                            var get_org = (from a in dbTNC.View_Organization
                                           where a.dept_id == item.org
                                           select a).FirstOrDefault();
                            if (get_org != null && ulv == 4 && get_org.plant_id == org)//Plant instead of Dept.
                            {
                                ViewBag.CurrentTran = item;
                            }
                        }
                    }
                    else//Other : HQ, Vender
                    {
                    }

                    if (ViewBag.CurrentTran != null)
                    {
                        if (item.status_id == 1 && item.comment == "Update PR")
                        {
                            ViewBag.ActionForm = "EditPR";
                        }
                        else if (item.status_id < 5)
                        {
                            if (ulv <= 2)//Mgr. Down
                            {
                                ViewBag.ActionForm = "_FormApvRej1";
                            }
                            else //Dept. Up
                            {
                                ViewBag.ActionForm = "_FormApvRejCan1";
                            }
                        }
                        //else if (item.status_id == 4)
                        //{
                        //    ViewBag.ActionForm = "_FormSendPR";
                        //}

                        break;
                    }
                }
            }

            return PartialView();
        }

        [HttpGet]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _TabFiles(string pr_no)
        {
            var get_files = TntDb.td_files.Where(w => w.pr_no == pr_no).OrderBy(o => o.file_type);
            return PartialView(get_files);
        }

        [HttpGet]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _TabTNC(string pr_no)
        {
            var get_tran = TntDb.v_tran_show.Where(w => w.pr_no == pr_no).OrderBy(o => o.act_dt);
            //string orgname = "";
            //dbTNC.sp_orgname_by_org_lv(1, 1, orgname);
            return PartialView(get_tran);
        }

        [HttpPost]
        public ActionResult ApproveAction(string pr_no, int torg, byte tulv, byte status, byte rev, string comment)
        {
            try
            {
                string actor = Session["TNT_Auth"].ToString();
                byte ulv = byte.Parse(Session["TNT_ULv"].ToString());
                int org = int.Parse(Session["TNT_Org"].ToString());

                //string pr_no = Request.Form["hdpr"];
                //int torg = int.Parse(Request.Form["hdorg"]);
                //byte tulv = byte.Parse(Request.Form["hdulv"]);
                //byte status = byte.Parse(Request.Form["hdstt"]);
                //byte rev = byte.Parse(Request.Form["hdrev"]);
                //string comment = Request.Form["comment"];

                string act = "APV";

                //Send PR
                if (status == 4)
                {
                    TransferPRtoPO(pr_no, "111080");//Real:actor, Test:"111080"
                }

                if (ulv != tulv)
                {
                    UpdateTran(pr_no, status, tulv, torg, rev, action: "SKIP");
                    CreateTran(pr_no, status, ulv, org, rev, actor, act, comment);
                }
                else
                {
                    UpdateTran(pr_no, status, tulv, torg, rev, actor, act, comment);
                }

                ManageTran(pr_no, status, ulv, org, rev, actor, act, comment);
                TntDb.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["noty_warn"] = "Approve failed !!!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult RejectAction(string pr_no, int torg, byte tulv, byte status, byte rev, string comment)
        {
            try
            {
                string actor = Session["TNT_Auth"].ToString();
                byte ulv = byte.Parse(Session["TNT_ULv"].ToString());
                int org = int.Parse(Session["TNT_Org"].ToString());

                string act = "REJ";

                if (ulv != tulv)
                {
                    UpdateTran(pr_no, status, tulv, torg, rev, action: "SKIP");
                    CreateTran(pr_no, status, ulv, org, rev, actor, act, comment);
                }
                else
                {
                    UpdateTran(pr_no, status, tulv, torg, rev, actor, act, comment);
                }

                ManageTran(pr_no, status, ulv, org, rev, actor, act, comment);
                TntDb.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["noty_warn"] = "Reject failed !!!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CancelAction(string pr_no, int torg, byte tulv, byte status, byte rev, string comment)
        {
            try
            {
                string actor = Session["TNT_Auth"].ToString();
                byte ulv = byte.Parse(Session["TNT_ULv"].ToString());
                int org = int.Parse(Session["TNT_Org"].ToString());

                string act = "CANC";

                if (ulv != tulv)
                {
                    UpdateTran(pr_no, status, tulv, torg, rev, action: "SKIP");
                    CreateTran(pr_no, status, ulv, org, rev, actor, act, comment);
                }
                else
                {
                    UpdateTran(pr_no, status, tulv, torg, rev, actor, act, comment);
                }

                ManageTran(pr_no, status, ulv, org, rev, actor, act, comment);
                TntDb.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["noty_warn"] = "Cancel failed !!!";
                return RedirectToAction("Index", "Home");
            }
        }

        private void TransferPRtoPO(string pr_no, string actor)
        {
            using (var prdb = new PREntities())
            {
                var query = from a in TntDb.td_tooling
                            where a.pr_no == pr_no && a.sell == false
                            select a;

                foreach (var item in query)
                {
                    PR_TO_POEDI po = new PR_TO_POEDI();
                    //PR_TO_POEDI_TRAIN po = new PR_TO_POEDI_TRAIN();
                    var dt = DateTime.Now;
                    po.pr_no = item.pr_no;
                    po.pr_line = item.pr_line.ToString();
                    po.isTransfer = "0";
                    po.CreateDate = dt;
                    //po.UploadDate =
                    po.PIC = "T" + actor;
                    po.vendor = "";
                    po.item = item.td_pr.item_code.ToUpper();
                    po.description = item.description.ToUpper();
                    po.QTY = item.qty;
                    po.unitPack = item.unit;
                    po.duedate = item.td_pr.due_date;
                    po.investmentNo = "";
                    po.investmentName = "";
                    po.accountCode = "";//Import-Export Fill in NBCS
                    po.costCode = item.td_pr.cost_code;
                    po.costCodeNote = "";
                    po.unitPrice = 0;
                    po.brand = "";
                    po.quotation = "";
                    po.ApprovedDate = dt.Date;
                    po.ApprovedTime = dt.TimeOfDay;
                    po.IssueBy = "ISSUED BY: " + item.td_pr.issue_name;
                    po.IssueGroup = item.td_pr.issue_group_name + " / TEL: " + item.td_pr.ext;
                    po.CostNote = "";
                    po.Model = "";
                    po.WONo = "";
                    po.Purpose = "";
                    po.vQuoNo = "";
                    po.Sparepart = "";
                    po.MTC = "";
                    po.PRUser = item.td_pr.issue_name;
                    po.ProjectName = "";
                    po.Reason = "";

                    prdb.PR_TO_POEDI.Add(po);
                    //prdb.PR_TO_POEDI_TRAIN.Add(po);
                }
                prdb.SaveChanges();
            }
        }

        public void SendEmailCenter(string pr_no, string receiver, int type = 0, string content = "", string status = "")
        {
            string actor = Session["TNT_Auth"].ToString();
            string act_name = Session["TNT_Name"].ToString();
            if (!string.IsNullOrEmpty(receiver))
            {
                string mailto = "";
                char[] delimiter = new char[] { ',' };
                string[] email = receiver.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                int max_email = 47;
                if (email.Length > max_email)//Max send email = 50
                {
                    for (int i = 0; i < max_email; i++)
                    {
                        mailto += "," + email[i];
                    }
                    mailto = mailto.Substring(1);
                }
                else
                {
                    mailto = receiver;
                }

                var get_pr = TntDb.td_pr.Find(pr_no);

                if (get_pr != null)
                {
                    TNCUtility tnc_util = new TNCUtility();
                    string subject = "";
                    //string body = "";//For Real
                    string body = "Mail :" + mailto + "<br />";//For Test
                    string int_link = "http://webExternal/TNT/Home";
                    //string ext_link = "http://webexternal.nok.co.th/TNT/Home";

                    if (type == 0)//waiting for process
                    {
                        subject = "PR No. " + pr_no + " waiting for process";
                        body += "Dear. All Concern,<br /><br />" +
                            "You have PR No. <b>" + pr_no + "</b> waiting for Process at Status : <b>" + status + "</b><br />" +
                            "<br /><a href='" + int_link + "/index?id=" + pr_no + "'>TNC-NOK Tooling</a><br />";
                    }
                    else if (type == 1)//Email Reject
                    {
                        subject = "PR No. " + pr_no + " was Rejected";
                        body += "Dear. All Concern,<br /><br />" +
                            "You have PR No. <b>" + pr_no + "</b> was Reject as detail below<br />" +
                            "Reject by : " + act_name + "<br />" +
                            "Reason : " + content + "<br />" +
                            "<br /><a href='" + int_link + "/index?id=" + pr_no + "'>TNC-NOK Tooling</a><br />";
                    }
                    else if (type == 2)//Email Feedback
                    {
                        subject = "Feedback for PR No. " + pr_no;
                        body += "Dear. All Concern,<br /><br />" +
                            "You have feed back of PR No. <b>" + pr_no + "</b> as detail below<br />" +
                            "Feedback by : " + act_name + "<br />" +
                            "Feedback detail : " + content + "<br />" +
                            "<br /><a href='" + int_link + "/index?id=" + pr_no + "'>TNC-NOK Tooling</a><br />";
                    }
                    else if (type == 3)//Email Close
                    {
                        subject = "Packing Change online was Closed (" + pr_no + ")";
                        body += "Dear. All Concern,<br /><br />" +
                            "PR No. <b>" + pr_no + "</b> was Closed as detail below<br />" +
                            "<a href='" + int_link + "/index?id=" + pr_no + "'>TNC-NOK Tooling</a><br />";
                    }
                    else if (type == 4)//Email Cancel
                    {
                        subject = "PR No. " + pr_no + " was Cancelled";
                        body += "Dear. All Concern,<br /><br />" +
                            "You have Packing Change No. <b>" + pr_no + "</b> was cancel as detail below<br />" +
                            "Cancel by : " + act_name + "<br />" +
                            "Reason : " + content + "<br />" +
                            "<br /><a href='" + int_link + "/index?id=" + pr_no + "'>TNC-NOK Tooling</a><br />";
                    }
                    else if (type == 5)//Email Revise
                    {
                        subject = "PR No. " + pr_no + " was sent back for revision.";
                        body += "Dear. All Concern,<br /><br />" +
                            "You have PR No. <b>" + pr_no + "</b> was revised as detail below<br />" +
                            "by : " + act_name + "<br />" +
                            "Reason : " + content + "<br />" +
                            "<br /><a href='" + int_link + "/index?id=" + pr_no + "'>TNC-NOK Tooling</a><br />";
                    }

                    body += "<br /><b>Best Regard.<br />" + "From TNC-NOK Tooling System</b>";

                    //tnc_util.SendMail(41, "TNCAutoMail-TNT@nok.co.th", mailto, subject, body);//For Real
                    tnc_util.SendMail(41, "TNCAutoMail-TNT@nok.co.th", "monchit@nok.co.th", subject, body);//For Test
                }
            }
        }

        //public string GetOrgnameByOrgLv(int org, byte lv = 1)
        //{
        //    string orgname = "";

        //    if (lv < 3)
        //    {
        //        orgname = dbTNC.tnc_group_master.Find(org).group_name;
        //    }
        //    else if (lv == 3)
        //    {
        //        orgname = dbTNC.tnc_dept_master.Find(org).dept_name;
        //    }
        //    else if (lv == 4)
        //    {
        //        orgname = dbTNC.tnc_plant_master.Find(org).plant_name;
        //    }
        //    else if (lv == 5)
        //    {
        //        orgname = "Managing Director";
        //    }

        //    return orgname;
        //}
    }
}