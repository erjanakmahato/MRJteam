using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using WebMatrix.WebData;
using System.Configuration;
using System.Net;

namespace MRJTeam.Controllers
{
    public class ForgetPassController : Controller
    {
        // GET: ForgetPass
        public ActionResult ForgetPassword()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult ForgotPassword(string UserName)
        {
            if (ModelState.IsValid)
            {

                if (WebSecurity.UserExists(UserName))
                {
                    string To = UserName, UserID, Password, SMTPPort, Host;
                    string token = WebSecurity.GeneratePasswordResetToken(UserName);
                    if (token == null)
                    {
                        // If user does not exist or is not confirmed.  

                        return View("Index");

                    }
                    else
                    {
                        //Create URL with above token  

                        var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = UserName, code = token }, "http") + "'>Reset Password</a>";


                        //HTML Template for Send email  

                        string subject = "Your changed password";

                        string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;


                        //Get and set the AppSettings using configuration manager.  

                        EmailManager.AppSettings(out UserID, out Password, out SMTPPort, out Host);


                        //Call send email methods.  

                        EmailManager.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);

                    }

                }

            }
            return View();
        }
        public class EmailManager
        {
            public static void AppSettings(out string UserID, out string Password, out string SMTPPort, out string Host)
            {
                UserID = ConfigurationManager.AppSettings.Get("UserID");
                Password = ConfigurationManager.AppSettings.Get("Password");
                SMTPPort = ConfigurationManager.AppSettings.Get("SMTPPort");
                Host = ConfigurationManager.AppSettings.Get("Host");
            }
            public static void SendEmail(string From, string Subject, string Body, string To, string UserID, string Password, string SMTPPort, string Host)
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(To);
                mail.From = new MailAddress(From);
                mail.Subject = Subject;
                mail.Body = Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;
                smtp.Port = Convert.ToInt16(SMTPPort);
                smtp.Credentials = new NetworkCredential(UserID, Password);
                smtp.EnableSsl = true;
                smtp.Send(mail);



                //web.config ma 4 otta key rw value add gareka xauu
            }
        }
    }
}