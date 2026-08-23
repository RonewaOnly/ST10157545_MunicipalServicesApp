using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    public class IssueRepository
    {
        private static readonly List<Issue> _issues = new List<Issue>();

        public static List<Issue> Issues
        {
            get { return _issues; }
        }

        public static void Add(Issue issue)
        {
            _issues.Add(issue);
        }

        public static int Count { get { return _issues.Count; } }
    }
}
