using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPOFixed
{
    class Program
    {
        static void Main(string[] args)
        {
            PREntities dbpr = new PREntities();
            TncNokToolingEntities dbtnt = new TncNokToolingEntities();

            try
            {
                var tnt_get = from a in dbtnt.v_tran
                              where a.status_id == 5 && a.ulv_id == 1 && a.po_no == null
                              select a;

                foreach (var item in tnt_get)
                {
                    //Console.WriteLine("v_tran:" + item.pr_no);
                    var get_max_appcount = (from a in dbpr.PO_APP_LINE
                                            where a.AppSign == "2" && a.PRNO == item.pr_no
                                            group a by a.PRNO into g
                                            select new { prno = g.Key, max_app = g.Max(m => m.AppCount) }).FirstOrDefault();

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
                            addTran.status_id = 13;
                            addTran.ulv_id = 1;
                            addTran.org = 133;
                            addTran.rev = updTran.rev;
                            addTran.act_dt = DateTime.Now;
                            dbtnt.td_tran.Add(addTran);
                        }
                    }
                    else
                    {
                        var updTran = dbtnt.td_tran.First(w => w.pr_no == item.pr_no && w.status_id == 5 && w.ulv_id == 1 && w.act_id == null);
                        if (updTran != null)
                        {
                            updTran.act_id = "SKIP";
                            updTran.act_dt = DateTime.Now;

                            td_tran addTran = new td_tran();
                            addTran.pr_no = item.pr_no;
                            addTran.status_id = 100;//Completed
                            addTran.ulv_id = 1;
                            addTran.org = 0;
                            addTran.rev = updTran.rev;
                            addTran.act_dt = DateTime.Now;
                            dbtnt.td_tran.Add(addTran);
                        }
                    }
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
