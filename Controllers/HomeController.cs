using Falcon_IssueTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Falcon_IssueTracker.Controllers
{
    public class HomeController : Controller
    {       
        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }   
        
        public ActionResult Peter()
        {
            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ContactAsync(EmailModel model)
        {
            try
            {
                //The email To in this case is to me from someone else
                var emailAddress = WebConfigurationManager.AppSettings["EmailTo"];
                var emailFrom = $"{model.From}<{emailAddress}>";
                var FinalBody = $"{model.Name} has sent you the following message <br /> {model.Body} <hr /> {WebConfigurationManager.AppSettings["LegalText"]}";

                var email = new MailMessage(emailFrom, emailAddress)
                {
                    Subject = model.Subject,
                    Body = FinalBody,
                    IsBodyHtml = true
                };

                //Spin up an instance of the EmailService Class 

                var emailSvc = new EmailService();
                await emailSvc.SendAsync(email);

                var personalEmailSvc = new PersonalEmail();
                await personalEmailSvc.SendAsync(email);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }

            return View(new EmailModel());
        }                 
    }
}