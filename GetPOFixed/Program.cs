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
                var appline = from a in dbpr.PO_APP_LINE
                              where a.PRNO.Contains("TNT") && a.AppSign != "2" && (a.POStatus == "O" || a.POStatus == "C")
                              orderby a.PONO ascending
                              select a;

                var temp = "";

                foreach (var item in appline)
                {
                    if (temp != item.PONO)
                    {
                        var apphead = (from b in dbpr.PO_APP_HEAD
                                       where b.PONO == item.PONO && b.AppSign != "2"
                                       orderby b.AppCount descending
                                       select b).FirstOrDefault();

                        if (apphead != null)
                        {
                            var updTran = dbtnt.td_tran.First(w => w.pr_no == item.PRNO && w.status_id == 5 && w.ulv_id == 1 && w.org == 0);
                            if (updTran != null)
                            {
                                updTran.act_id = "SKIP";

                                td_tran addTran = new td_tran();
                                addTran.pr_no = item.PRNO;
                                addTran.status_id = 6;
                                addTran.ulv_id = 2;
                                addTran.org = 133;
                                addTran.rev = updTran.rev;
                                addTran.act_dt = DateTime.Now;
                                dbtnt.td_tran.Add(addTran);

                                dbtnt.SaveChanges();
                            }
                        }
                    }
                    temp = item.PONO;
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Error Get PO Fixed.");
                Console.ReadLine();
            }
        }
    }
}
