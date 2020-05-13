
using Falcon_IssueTracker.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Falcon_IssueTracker.Helpers
{
    public class ProjectsHelper
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public bool IsUserOnProject(string userId, int projectId) 
        { 
            var project = db.Projects.Find(projectId); 
            var flag = project.Members.Any(u => u.Id == userId); 
            return (flag); 
        }

        public ICollection<Project> ListUserProjects(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var projects = user.Projects.ToList(); 
            return (projects);
        }

        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsUserOnProject(userId, projectId))
            {
                var proj = db.Projects.Find(projectId); 
                var newUser = db.Users.Find(userId);

                proj.Members.Add(newUser); 
                db.SaveChanges();
            }
        }

        public void RemoveUserFromProject(string userId, int projectId)
        {
            if (IsUserOnProject(userId, projectId))
            {
                var proj = db.Projects.Find(projectId); 
                var delUser = db.Users.Find(userId); 

                proj.Members.Remove(delUser);
                db.SaveChanges();
            }
        }

        public ICollection<ApplicationUser> UsersOnProject(int projectId) 
        { 
            return db.Projects.Find(projectId).Members; 
        }

        public ICollection<ApplicationUser> UsersNotOnProject(int projectId) 
        { 
            return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList(); 
        }
    }
}