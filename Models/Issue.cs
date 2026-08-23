using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    public class Issue
    {
        public Guid Id { get; private set; }
        public string Location { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public List<string> AttachmentPaths { get; private set; }

        public DateTime DateReported { get; private set; }

        public Issue(string location, string category, string description, List<string> attachmentPaths)
        {
            Id = Guid.NewGuid();
            Location = location;
            Category = category;
            Description = description;
            AttachmentPaths = attachmentPaths ?? new List<string>();
            DateReported = DateTime.Now;
        }
    }
}
