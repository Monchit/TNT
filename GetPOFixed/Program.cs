using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommonFunction;

namespace GetPOFixed
{
    class Program
    {
        static void Main(string[] args)
        {
            PREntities dbpr = new PREntities();
            TncNokToolingEntities dbtnt = new TncNokToolingEntities();
            MailCenterEntities dbMail = new MailCenterEntities();

            try
            {
                //---------------Get Email Admin-----------------//

                var get_mail = from a in dbtnt.tm_user_nok
                               where a.active == true && !string.IsNullOrEmpty(a.email)
                               select a.email;
                string mailto = "";

                foreach (var x in get_mail)
                {
                    mailto += ", " + x;
                }
                mailto = mailto.Substring(2);

                //---------------Manage Tran-----------------//

                var tnt_get = from a in dbtnt.v_tran
                              where a.status_id == 5 && a.ulv_id == 1 && a.po_no == null
                              select a;
                
                foreach (var item in tnt_get)
                {
                    var get_po = (from a in dbpr.PO_APP_LINE
                                  where a.AppSign == "2" && a.POStatus == "O" && a.PRNO == item.pr_no
                                  select a).FirstOrDefault();

                    if (get_po != null)
                    {
                        var pr = dbtnt.td_pr.Find(item.pr_no);
                        pr.po_no = get_po.PONO;
                        Console.WriteLine(get_po.PRNO + " : " + get_po.PONO);

                        var updTran = dbtnt.td_tran.First(w => w.pr_no == item.pr_no && w.status_id == 5 && w.ulv_id == 1 && w.act_id == null);
                        if (updTran != null)
                        {
                            updTran.act_id = "SKIP";
                            updTran.act_dt = DateTime.Now;

                            td_tran addTran = new td_tran();
                            addTran.pr_no = item.pr_no;
                            addTran.status_id = 81;//81=Wait for NOK receive PO, 82=Already received PO from TNC
                            addTran.ulv_id = 0;
                            addTran.org = 0;
                            addTran.rev = updTran.rev;
                            addTran.act_dt = DateTime.Now;
                            dbtnt.td_tran.Add(addTran);

                            //-----------------Send Email------------------//

                            string subject = "[" + get_po.PONO + "] New PO from TNC";
                            string buyer = get_po.PO_APP_HEAD.Buyer.Trim();
                            string issue_by = "";
                            if (buyer == "T206497")
                            {
                                issue_by = "Sailom P.";
                            }
                            else if (buyer == "T206263")
                            {
                                issue_by = "Jaruporn W.";
                            }

                            //var get_iss_email = dbtnt.td_tran.Where(w => w.pr_no == item.pr_no && w.act_id == "ISS")
                            //                    .Select(s => s.actor).FirstOrDefault();

                            string ext_link = "http://webexternal.nok.co.th/TNT/Home/NOKSearch?id=" + get_po.PONO;
                            string body = "Dear All, <br />You have new PO from Thai NOK Co.,Ltd.<br />Issue by : " + issue_by
                                        + "<br />Information list below<br />PO No. " + get_po.PONO
                                        + "<a href='" + ext_link + "'> [>>View Detail Here<<]</a>"
                                        + "<br /><br />Best Regards,<br />From TNC Tooling Web";

                            TNCUtility tnc_util = new TNCUtility();

                            tnc_util.SendMail(41, "TNCAutoMail-TNT@nok.co.th", mailto, subject, body, "");//For Real
                            //tnc_util.SendMail(41, "TNCAutoMail-TNT@nok.co.th", "monchit@nok.co.th", subject, body);//For Test
                        }
                    }
                    //else
                    //{
                    //    var updTran = dbtnt.td_tran.First(w => w.pr_no == item.pr_no && w.status_id == 5 && w.ulv_id == 1 && w.act_id == null);
                    //    if (updTran != null)
                    //    {
                    //        updTran.act_id = "SKIP";
                    //        updTran.act_dt = DateTime.Now;

                    //        td_tran addTran = new td_tran();
                    //        addTran.pr_no = item.pr_no;
                    //        addTran.status_id = 100;//Completed
                    //        addTran.ulv_id = 1;
                    //        addTran.org = 0;
                    //        addTran.rev = updTran.rev;
                    //        addTran.act_dt = DateTime.Now;
                    //        dbtnt.td_tran.Add(addTran);
                    //    }
                    //}
                }
                dbtnt.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error Get PO Fixed.");
                Console.ReadLine();
            }
        }
    }
}
