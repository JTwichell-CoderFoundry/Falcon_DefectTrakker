using Falcon_IssueTracker.Helpers;
using Falcon_IssueTracker.Models;
using Falcon_IssueTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Falcon_IssueTracker.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper roleHelper = new RolesHelper();

        // GET: Admin
        public ActionResult ManageRoles()
        {
            var viewData = new List<CustomUserData>();
            var users = db.Users.ToList();
            foreach(var user in users)
            {
                //viewData.Add(new UserRoleData {
                //    FirstName = user.FirstName,
                //    LastName = user.LastName,
                //    Email = user.Email,
                //    RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault() ?? "UnAssigned"
                //});

                var newUserData = new CustomUserData();

                newUserData.FirstName = user.FirstName;
                newUserData.LastName = user.LastName;
                newUserData.Email = user.Email;
                newUserData.RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault() ?? "UnAssigned";
              
                viewData.Add(newUserData);             
            }

            //Right hand side control: This data will be used to power a Dropdown List in the View
            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name");

            //Left hand side control: This data will be used to power ListBox in the View
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");

            return View(viewData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string roleName)
        {
            //This action is intended to operate on the selected Users. 
            //If no Users were selected then there is nothing to do
            if(userIds != null)
            {
                foreach(var userId in userIds)
                {
                    //var currentRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
                    //roleHelper.RemoveUserFromRole(userId, currentRole);
                    
                    roleHelper.RemoveUserFromRole(userId, roleHelper.ListUserRoles(userId).FirstOrDefault());

                    if (!string.IsNullOrEmpty(roleName))
                        roleHelper.AddUserToRole(userId, roleName);
                }
            }

            return RedirectToAction("ManageRoles");
        }




    }
}