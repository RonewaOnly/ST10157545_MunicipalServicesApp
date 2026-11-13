using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;

namespace ST10157545_MunicipalServicesApp.Models
{
    /// <summary>
    /// Represents a single municipal issue reported by a citizen. As of Part
    /// 3, every Issue also doubles as the "service request" the brief asks
    /// for: it carries a sequential ticket number, a Status that moves from
    /// Submitted through to Closed, an auto-assigned Priority, and space for
    /// municipal staff to log a response. Instances of this class are stored
    /// in the in-memory IssueRepository data structure (a List&lt;Issue&gt;,
    /// plus several index structures built on top of it) for the lifetime of
    /// the application - there is no database behind any of it.
    /// </summary>
    public class Issue
    {
        public Guid Id { get; private set; }
        public int TicketNumber { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> AttachmentPaths { get; private set; }
        public DateTime DateReported { get; set; }
        public string CreatedByUsername { get; set; }

        public RequestStatus Status { get; set; }
        public RequestPriority Priority { get; set; }
        public string StaffResponse { get; set; }
        public string RespondedByUsername { get; set; }
        public DateTime? DateResponded { get; set; }

        public Issue(string location, string category, string description, List<string> attachmentPaths)
        {
            Id = Guid.NewGuid();
            Location = location;
            Category = category;
            Description = description;
            AttachmentPaths = attachmentPaths ?? new List<string>();
            DateReported = DateTime.Now;
            Status = RequestStatus.Submitted;
            Priority = DerivePriority(category);
        }

        /// <summary>
        /// Maps a category to a default urgency so Priority can be assigned
        /// the moment a request is created, without needing staff input or a
        /// second pass that would force the priority-keyed Red-Black Tree and
        /// urgency heap to be rebuilt.
        /// </summary>
        private static RequestPriority DerivePriority(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) return RequestPriority.Medium;

            switch (category.Trim())
            {
                case "Water & Sanitation":
                case "Public Safety":
                    return RequestPriority.Critical;
                case "Electricity":
                    return RequestPriority.High;
                case "Roads & Stormwater":
                    return RequestPriority.Medium;
                case "Waste Management":
                case "Parks & Recreation":
                    return RequestPriority.Low;
                default:
                    return RequestPriority.Medium;
            }
        }

        public string TicketReference
        {
            get { return "SR-" + TicketNumber.ToString("D5"); }
        }
    }
}
