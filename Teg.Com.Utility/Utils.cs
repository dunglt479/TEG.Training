using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Teg.Com.Utility
{
    public static class Utils
    {
        public static T Singleton<T>(string prefix ="")
        {
            // To support engine side calls
            if (HttpContext.Current == null)
            {
                return Xingleton<T>();
            }

            var k = prefix + typeof(T).Name;
            var o = HttpContext.Current.Items[k];

            if (o == null)
            {
                o = Activator.CreateInstance(typeof(T), true);
                HttpContext.Current.Items[k] = o;
            }

            var res = (T)HttpContext.Current.Items[k];
            return res;
        }

        public static T Xingleton<T>(string prefix="")
        {
            var k = prefix + typeof(T).Name;

            if (_instances == null)
            {
                _instances = new Dictionary<string, object>();
            }

            var exists = _instances.Keys.Contains(k);
            if (!exists)
            {
                var o = Activator.CreateInstance(typeof(T), true);
                _instances.Add(k, o);
            }

            var res = (T)_instances[k];
            return res;
        }
        private static Dictionary<string, object> _instances;

        public static string GetEmailTemplate(string templateFileName)
        {
            if (!string.IsNullOrEmpty(templateFileName))
            {
                var fileTemplate = HttpContext.Current.Server.MapPath("~/EmailTemplate/"
                    + templateFileName);
                if (File.Exists(fileTemplate))
                {
                    var contentTemplate = File.ReadAllText(fileTemplate);
                    return contentTemplate;
                }
            }

            return string.Empty;
        }
        public static void SendEmailTest(string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,string account,string password)
        {
            try
            {
                var message = new MailMessage();
                //from, to, reply to
                message.From = new MailAddress(fromAddress, fromName);
                message.To.Add(new MailAddress(toAddress, fromName));
                //content
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                //send email
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.googlemail.com";
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(account, password);
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

