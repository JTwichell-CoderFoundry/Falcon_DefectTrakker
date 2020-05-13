using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Falcon_IssueTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        //Nav props
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }

        public Project()
        {
            Tickets = new HashSet<Ticket>();
            Members = new HashSet<ApplicationUser>();
        }

    }
}