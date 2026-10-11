using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    public class Event
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public Event(string title, string category, DateTime date, string locatiion, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Category = category;
            Date = date;
            Location = locatiion;
            Description = description;
        }

        public override string ToString()
        {
            return string.Format("{0:ddd, dd MMM yyyy} -{1} ({2})", Date,Title,Category);
        }
    }
}
