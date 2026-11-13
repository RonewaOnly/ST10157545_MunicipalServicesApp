using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10157545_MunicipalServicesApp.Models
{
    /// <summary>
    /// Central in-memory repository for every reported issue / service
    /// request. Still just a List&lt;Issue&gt; at heart (no database of any
    /// kind), but Part 3 layers several additional collections on top of
    /// that list so the Service Request Status page can satisfy the brief's
    /// Technical Requirements without ever re-scanning the full list:
    ///
    ///   - BinarySearchTree&lt;Issue&gt;  keyed by ticket number  -> "find my ticket"
    ///   - AVLTree&lt;Issue&gt;            keyed by date reported  -> balanced chronological listing
    ///   - RedBlackTree&lt;Issue&gt;       keyed by priority+ticket -> balanced listing by urgency band
    ///   - RequestUrgencyHeap&lt;Issue&gt; keyed by urgency score   -> "top N most urgent" without a full sort
    ///   - HashSet&lt;string&gt;           of wards with an open request -> feeds the LocationGraph MST
    ///
    /// None of the keys above (ticket number, date, priority) ever change
    /// after a request is created, so none of these index structures need a
    /// delete/re-insert when staff update Status or add a response.
    /// </summary>
    public static class IssueRepository
    {
        private static readonly List<Issue> _issues = new List<Issue>();
        private static int _nextTicketNumber = 1001;

        private static readonly BinarySearchTree<Issue> _byTicketNumber = new BinarySearchTree<Issue>();
        private static readonly AVLTree<Issue> _byDateReported = new AVLTree<Issue>();
        private static readonly RedBlackTree<Issue> _byPriority = new RedBlackTree<Issue>();
        private static readonly RequestUrgencyHeap<Issue> _urgencyHeap = new RequestUrgencyHeap<Issue>();

        static IssueRepository()
        {
            SeedDemoIssues();
        }

        /// <summary>
        /// Demo data so the Service Request Status screens are populated the
        /// first time the app runs (there is no database to load from). All
        /// of these belong to the seeded "citizen" demo account.
        /// </summary>
        private static void SeedDemoIssues()
        {
            AddSeed("Ward 1 - Central CBD", "Water & Sanitation", "Burst water main flooding the pavement outside the taxi rank.", 9, RequestStatus.InProgress,
                "A maintenance crew has been dispatched and the main has been isolated.", "staff");
            AddSeed("Ward 3 - Hillview", "Electricity", "Streetlights have been out for a week along Protea Road.", 7, RequestStatus.Acknowledged, null, null);
            AddSeed("Ward 2 - Riverside", "Roads & Stormwater", "Large pothole near the river bridge damaging vehicles.", 6, RequestStatus.Submitted, null, null);
            AddSeed("Ward 5 - Oakdale", "Waste Management", "Refuse not collected for two weeks on Oak Street.", 5, RequestStatus.Resolved,
                "Collection completed on Tuesday. Please report again if it recurs.", "admin");
            AddSeed("Ward 7 - Lakeview", "Public Safety", "Broken fence at the lakeside walkway is a danger to children.", 4, RequestStatus.Submitted, null, null);
            AddSeed("Ward 4 - Greenfields", "Parks & Recreation", "Playground swings are broken and the grass has not been cut.", 3, RequestStatus.Acknowledged, null, null);
            AddSeed("Ward 8 - Industria", "Electricity", "Power outage affecting several factories on Mill Road.", 2, RequestStatus.InProgress,
                "Faulty transformer identified; replacement scheduled.", "staff");
            AddSeed("Ward 6 - Sunnyside", "Water & Sanitation", "Sewage overflow in the stormwater drain on Sunny Lane.", 1, RequestStatus.Submitted, null, null);
        }

        private static void AddSeed(string location, string category, string description, int daysAgo,
            RequestStatus status, string response, string respondedBy)
        {
            Issue issue = new Issue(location, category, description, null);
            issue.CreatedByUsername = "citizen";
            issue.DateReported = DateTime.Now.AddDays(-daysAgo).AddMinutes(-daysAgo * 7);
            issue.Status = status;
            Add(issue);

            if (response != null)
            {
                RecordResponse(issue, response, respondedBy);
            }
        }

        public static List<Issue> Issues
        {
            get { return _issues; }
        }

        public static void Add(Issue issue)
        {
            issue.TicketNumber = _nextTicketNumber++;
            _issues.Add(issue);

            _byTicketNumber.Insert(issue.TicketNumber, issue);
            _byDateReported.Insert(issue.DateReported.Ticks, issue);

            long priorityKey = ((long)issue.Priority * 10_000_000L) + issue.TicketNumber;
            _byPriority.Insert(priorityKey, issue);

            // Priority dominates; within a priority band the older request scores higher.
            long secondsSince2000 = (issue.DateReported.Ticks - new DateTime(2000, 1, 1).Ticks) / TimeSpan.TicksPerSecond;
            long urgencyScore = ((long)issue.Priority * 10_000_000_000L) - secondsSince2000;
            _urgencyHeap.Insert(urgencyScore, issue);
        }

        public static int Count
        {
            get { return _issues.Count; }
        }

        public static bool TryFindByTicketNumber(int ticketNumber, out Issue issue)
        {
            return _byTicketNumber.TryFind(ticketNumber, out issue);
        }

        /// <summary>All requests in ticket-number order (BST in-order traversal).</summary>
        public static List<Issue> AllByTicketNumber()
        {
            return _byTicketNumber.InOrder();
        }

        /// <summary>All requests, most recently reported first (AVL in-order descending).</summary>
        public static List<Issue> AllByDateDescending()
        {
            return _byDateReported.InOrderDescending();
        }

        /// <summary>All requests, grouped so Critical/High priority appear first (Red-Black in-order descending).</summary>
        public static List<Issue> AllByPriorityDescending()
        {
            return _byPriority.InOrderDescending();
        }

        /// <summary>The N most urgent *open* (not Resolved/Closed) requests, without disturbing the heap.</summary>
        public static List<Issue> TopUrgentOpenRequests(int n)
        {
            // Extract from a copy of the heap in urgency order, skipping anything
            // already resolved/closed, and stop as soon as n open requests are found.
            return _urgencyHeap.PeekTopMatching(
                i => i.Status != RequestStatus.Resolved && i.Status != RequestStatus.Closed, n);
        }

        public static List<Issue> ByCitizen(string username)
        {
            return _issues
                .Where(i => string.Equals(i.CreatedByUsername, username, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(i => i.DateReported)
                .ToList();
        }

        /// <summary>Distinct wards (locations) that currently have at least one open request - used to build the MST.</summary>
        public static HashSet<string> WardsWithOpenRequests()
        {
            return new HashSet<string>(
                _issues.Where(i => i.Status != RequestStatus.Resolved && i.Status != RequestStatus.Closed)
                       .Select(i => i.Location),
                StringComparer.OrdinalIgnoreCase);
        }

        public static void UpdateStatus(Issue issue, RequestStatus newStatus)
        {
            issue.Status = newStatus;
        }

        public static void RecordResponse(Issue issue, string response, string staffUsername)
        {
            issue.StaffResponse = response;
            issue.RespondedByUsername = staffUsername;
            issue.DateResponded = DateTime.Now;
        }
    }
}
