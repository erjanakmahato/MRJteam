using MRJTeam.Models;
using MRJTeam.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace AdvanceLayoutPage.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        Entities _db = new Entities();
  public ActionResult Index()
        {
            List<UserViewModel> lst = new List<UserViewModel>();
            var Users = _db.tblUsers.ToList();
            foreach (tblUser item in Users)
            {
                lst.Add(new UserViewModel() { UserId = item.UserId, Username = item.Username, Password = item.Password, Fullname = item.Fullname });

            }

            return View(lst);
        }
        public JsonResult GetData()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<UserViewModel> lst = new List<UserViewModel>();
                var catList = db.tblUsers.ToList();
                foreach (var item in catList)
                {
                    lst.Add(new UserViewModel() { UserId = item.UserId, Username = item.Username, Usertype= item.Usertype, Fullname=item.Fullname });
                }
                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                using (Entities db = new Entities())
                {
                    ViewBag.Action = "New User";
                    return View(new UserViewModel());
                }
            }
            else
            {
                using (Entities db = new Entities())
                {
                    UserViewModel sub = new UserViewModel();
                    var menu = db.tblUsers.Where(x => x.UserId == id).FirstOrDefault();
                    sub.UserId = menu.UserId;
                    sub.Username = menu.Username;
                    sub.Usertype = menu.Usertype;
                    sub.Fullname = menu.Fullname;
                    

                    ViewBag.Action = "Edit User";
                    return View(sub);
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(UserViewModel sm)
        {
            using (Entities db = new Entities())
            {
                if (sm.UserId == 0)
                {
                    tblUser tb = new tblUser();
                    tb.Username = sm.Username;
                    tb.Usertype = sm.Usertype;
                    tb.Fullname = sm.Fullname;
                    db.tblUsers.Add(tb);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tblUser tbm = db.tblUsers.Where(m => m.UserId == sm.UserId).FirstOrDefault();
                    tbm.Username = sm.Username;
                    tbm.Usertype = sm.Usertype;
                    tbm.Fullname = sm.Fullname;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Entities db = new Entities())
            {
                tblUser sm = db.tblUsers.Where(x => x.UserId == id).FirstOrDefault();
                db.tblUsers.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }



        //[HttpGet]
        //public ActionResult ForgetPassword()
        //{
        //    return View();
        //}

        //[NonAction]
        //public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        //{
        //    var verifyUrl = "/User/" + emailFor + "/" + activationCode;
        //    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

        //    var fromEmail = new MailAddress("janakmahato9871@gmail.com", "Dotnet Awesome");
        //    var toEmail = new MailAddress(emailID);
        //    var fromEmailPassword = "9813079818jnk161316"; // Replace with actual password

        //    string subject = "";
        //    string body = "";
        //    if (emailFor == "VerifyAccount")
        //    {
        //        subject = "Your account is successfully created!";
        //        body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
        //            " successfully created. Please click on the below link to verify your account" +
        //            " <br/><br/><a href='" + link + "'>" + link + "</a> ";
        //    }
        //    else if (emailFor == "ResetPassword")
        //    {
        //        subject = "Reset Password";
        //        body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
        //            "<br/><br/><a href=" + link + ">Reset Password link</a>";
        //    }


        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
        //    };

        //    using (var message = new MailMessage(fromEmail, toEmail)
        //    {
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true
        //    })
        //        smtp.Send(message);
        //}


        //    [HttpPost]
        //public ActionResult ForgotPassword(string Email)
        //{
        //    //Verify Email ID
        //    //Generate Reset password link 
        //    //Send Email 
        //    string message = "";
        //    bool status = false;


        //    using (Entities db = new Entities())
        //    {
        //         var account = db.tblUsers.Where(a => a.Email == Email).FirstOrDefault();
               
        //        if (account != null)
        //        {
        //            //Send email for reset password
        //            string resetCode = Guid.NewGuid().ToString();
        //            SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
        //            account.ResetPasswordCode = resetCode;
        //            //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
        //            //in our model class in part 1
        //            db.Configuration.ValidateOnSaveEnabled = false;
        //            db.SaveChanges();
        //           message = "Reset password link has been sent to your email id.";
        //        }
        //        else
        //        {
        //            message = "Account not found";
        //        }
        //    }
        //    ViewBag.Message = message;
        //    return View();
        //}



        //public ActionResult ResetPassword(string id)
        //{
        //    //Verify the reset password link
        //    //Find account associated with this link
        //    //redirect to reset password page
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        return HttpNotFound();
        //    }

        //    using (Entities db = new Entities())
        //    {
        //        var user = db.tblUsers.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
        //        if (user != null)
        //        {
        //            ResetPasswordModel model = new ResetPasswordModel();
        //            model.ResetCode = id;
        //            return View(model);
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ResetPassword(ResetPasswordModel model)
        //{
        //    var message = "";
        //    if (ModelState.IsValid)
        //    {
        //        using (Entities db = new Entities())
        //        {
        //            var user = db.tblUsers.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
        //            if (user != null)
        //            {
        //                user.Password = model.NewPassword;
        //                user.ResetPasswordCode = "";
        //                db.Configuration.ValidateOnSaveEnabled = false;
        //                db.SaveChanges();
        //                message = "New password updated successfully";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        message = "Something invalid";
        //    }
        //    ViewBag.Message = message;
        //    return View(model);
        //}

    }
}