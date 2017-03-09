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
                            where a.act_id == null && a.status_id < 90
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

                //// Send mails via Smtp
                //string smtpServerIP = "10.201.31.239";
                //int port = 25;

                //// To
                //MailMessage mailMsg = new MailMessage();
                //string to = "itadmin@nok.co.th";
                //string from = "TNCAdmin@nok.co.th";

                //mailMsg.To.Add(to);
                ////if (!string.IsNullOrEmpty(CC))
                ////    mailMsg.CC.Add(CC);
                ////if (!string.IsNullOrEmpty(BCC))
                ////    mailMsg.Bcc.Add(BCC);

                //// From
                //MailAddress mailAddress = new MailAddress(from);
                //mailMsg.From = mailAddress;

                //// Subject and Body
                //mailMsg.Subject = "Test";
                //mailMsg.Body = "Test";
                //mailMsg.IsBodyHtml = true;

                //// Init SmtpClient and send
                //SmtpClient smtpClient = new SmtpClient(smtpServerIP, Convert.ToInt32(port));
                //smtpClient.Send(mailMsg);

                //Console.WriteLine("OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
        }
    }
}
