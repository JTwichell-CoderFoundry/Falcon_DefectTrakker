﻿using Falcon_IssueTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace Falcon_IssueTracker.Helpers
{
    public class RolesHelper
    {
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
       
        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        public ICollection<string> ListUserRoles(string userId)
        {
            return userManager.GetRoles(userId);
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            if (!string.IsNullOrEmpty(roleName)) 
            { 
                userManager.RemoveFromRole(userId, roleName); 
            }

            return true;
        }

        public ICollection<ApplicationUser> UsersInRole(string roleName)
        {
            //Create an empty list of Users to be filled eventually and returned
            var resultList = new List<ApplicationUser>();
            
            var List = userManager.Users.ToList(); 
            
            foreach (var user in List) 
            { 
                if (IsUserInRole(user.Id, roleName)) 
                    resultList.Add(user); 
            }

            return resultList;
        }

        public ICollection<ApplicationUser> UsersNotInRole(string roleName)
        {
            var resultList = new List<ApplicationUser>(); 
            var List = userManager.Users.ToList(); 
            
            foreach (var user in List) 
            { 
                if (!IsUserInRole(user.Id, roleName)) 
                    resultList.Add(user); 
            }

            return resultList;
        }
    }
}