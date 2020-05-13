using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Falcon_IssueTracker.Models
{
    public class Ticket
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        public int ProjectId { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketPriorityId { get; set; }
        public int TicketStatusId { get; set; }
        public string SubmitterId { get; set; }
        public string DeveloperId { get; set; }

        //Descriptive properties
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        //Start of Navigational props

        //Parents...
        public virtual Project Project { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual ApplicationUser Submitter { get; set; }
        public virtual ApplicationUser Developer { get; set; }

        //Children
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        //End of Navigational props

        //Constructor
        public Ticket()
        {
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();
        }

    }
}