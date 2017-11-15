using System;
using System.Net;
using System.Net.Mail;

namespace Qx.Scsxxt.Core.Services.Utility
{
    public class MailService
    {
        public  void Send(string ToMailAddress, string MailSubject, string MailBody,
        string ServerAddr = "Admin@scsxxt.com", string Host = "scsxxt.com",
         string SenderAddr = "Admin@scsxxt.com", string SenderName = "scsxxt.com"

        )
        {
            MailMessage mm = new MailMessage();
            //收件人地址
            mm.To.Add(new MailAddress(ToMailAddress));
            //发件人地址
            mm.From = new MailAddress(ServerAddr);//这个不要替换，这个跟服务器配置有关，以后再抽象
            //这个可以不指定
            mm.Subject = MailSubject;
            mm.Body = MailBody;
            //"<h3>This is Testing SMTP Mail Send By Me</h3>";
            //mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High; // 设置发送邮件的优先级
            SmtpClient smtCliend = new SmtpClient();
            //指定邮件服务器
            smtCliend.Host = Host;
            //smtp邮件服务器的端口号  
            smtCliend.Port = 25;
            smtCliend.UseDefaultCredentials = false;
            //设置发件人邮箱的用户名和地址，使用公共邮件服务器一般需要提供，不然发送不会成功
            smtCliend.Credentials = new NetworkCredential(SenderAddr, SenderName);
            //指定邮件的发送方式
            smtCliend.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtCliend.Send(mm);
                 Console.Write("Success!");
            }
            catch (SmtpException ex)
            {
                Console.Write(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }
    }
}
