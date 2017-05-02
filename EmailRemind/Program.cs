using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailRemind
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MailCenterEntities dbMail = new MailCenterEntities();
                TNC_ADMINEntities dbTNC = new TNC_ADMINEntities();
                TncNokToolingEntities dbTNT = new TncNokToolingEntities();

                var query = from a in dbTNT.td_tran
                            where a.act_id == null && a.status_id < 80
                            select a;

                foreach (var item in query)
                {
                    string email = "";

                    if (item.status_id == 5 || item.status_id == 6 || item.status_id == 7)//Import-Export
                    {
                        if (item.ulv_id == 1)
                        {

                        }
                        else if (item.ulv_id == 2)
                        {
                            var get_user = from a in dbTNT.tm_user
                                           where a.utype_id == 2
                                           select a;
                        }
                    }
                    else
                    {
                        var positionlv = (from p in dbTNT.tm_user_lv
                                          where p.id == item.ulv_id
                                          select p).FirstOrDefault();

                        if (positionlv != null)
                        {

                        }
                    }
                }

                //Console.WriteLine("OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
        }
    }
}
