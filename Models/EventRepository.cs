using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    public static class EventRepository
    {
        private static readonly List<Event> _events = new List<Event>();
        private static readonly EventPriorityQueue _upcomingQueue = new EventPriorityQueue();
        private static readonly SortedDictionary<DateTime, List<Event>> _eventsByDate = new SortedDictionary<DateTime, List<Event>>();
        private static readonly Dictionary<string, List<Event>> _eventsByCategory = new Dictionary<string, List<Event>>(StringComparer.OrdinalIgnoreCase);
        private static readonly HashSet<string> _categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private static readonly HashSet<DateTime> _uniqueDates = new HashSet<DateTime>();
        private static readonly Stack<Event> _recentlyViewed = new Stack<Event>();
        private static readonly Dictionary<string, int> _categorySearchFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        static EventRepository()
        {
            SeedSampleEvents();
        }
        public static IEnumerable<string> Categories
        {
            get { return _categories.OrderBy(c => c); }
        }
        public static IEnumerable<DateTime> UniqueDates
        {
            get { return _uniqueDates.OrderBy(d => d); }
        }
        public static void Add(Event newEvent)
        {
            _events.Add(newEvent);
            _upcomingQueue.Enqueue(newEvent, newEvent.Date);
            DateTime dateOnly = newEvent.Date.Date;
            List<Event> sameDate;
            if (!_eventsByDate.TryGetValue(dateOnly, out sameDate))
            {
                sameDate = new List<Event>();
                _eventsByDate[dateOnly] = sameDate;
            }
            sameDate.Add(newEvent);
            List<Event> sameCategory;
            if (!_eventsByCategory.TryGetValue(newEvent.Category, out sameCategory))
            {
                sameCategory = new List<Event>();
                _eventsByCategory[newEvent.Category] = sameCategory;
            }
            sameCategory.Add(newEvent);
            _categories.Add(newEvent.Category);
            _uniqueDates.Add(dateOnly);
        }
        /// <summary>
        /// Every event, ordered soonest-first via the priority queue.
        /// </summary>
        public static List<Event> GetUpcomingEvents()
        {
            return _upcomingQueue.ToOrderedList();
        }
        /// <summary>
        /// Searches events by an optional category, an optional specific
        /// date, and an optional free-text keyword matched against the title
        /// and description. Any combination of filters may be left empty.
        /// A matched category search is recorded for the recommendation
        /// engine (Design Consideration: user engagement/active participation).
        /// </summary>
        public static List<Event> Search(string category, DateTime? date, string keyword)
        {
            IEnumerable<Event> results = _events;
            if (!string.IsNullOrWhiteSpace(category) && !category.Equals("All Categories", StringComparison.OrdinalIgnoreCase))
            {
                List<Event> byCategory;
                results = _eventsByCategory.TryGetValue(category, out byCategory) ? byCategory : new List<Event>();
                RecordCategorySearch(category);
            }
            if (date.HasValue)
            {
                DateTime target = date.Value.Date;
                results = results.Where(e => e.Date.Date == target);
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string term = keyword.Trim();
                results = results.Where(e =>
                    e.Title.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    e.Description.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return results.OrderBy(e => e.Date).ToList();
        }
        public static void RecordCategorySearch(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return;
            }
            int current;
            _categorySearchFrequency[category] = _categorySearchFrequency.TryGetValue(category, out current)
                ? current + 1
                : 1;
        }
        public static void MarkViewed(Event viewedEvent)
        {
            _recentlyViewed.Push(viewedEvent);
        }
        public static List<Event> GetRecentlyViewed(int maxCount)
        {
            return _recentlyViewed.Take(maxCount).ToList();
        }
        /// <summary>
        /// Recommends events from whichever category the user has searched
        /// most often this session, excluding events already viewed. Returns
        /// an empty list until the user has performed at least one category
        /// search, since there is nothing yet to base a recommendation on.
        /// </summary>
        public static List<Event> GetRecommendations(int maxCount)
        {
            if (_categorySearchFrequency.Count == 0)
            {
                return new List<Event>();
            }
            string topCategory = _categorySearchFrequency
                .OrderByDescending(kvp => kvp.Value)
                .First()
                .Key;
            List<Event> candidates;
            if (!_eventsByCategory.TryGetValue(topCategory, out candidates))
            {
                return new List<Event>();
            }
            HashSet<Guid> viewedIds = new HashSet<Guid>(_recentlyViewed.Select(e => e.Id));
            return candidates
                .Where(e => !viewedIds.Contains(e.Id))
                .OrderBy(e => e.Date)
                .Take(maxCount)
                .ToList();
        }
        private static void SeedSampleEvents()
        {
            DateTime today = DateTime.Today;
            Add(new Event("Ward 12 Community Clean-Up", "Environment", today.AddDays(2), "Thokoza Park",
                "Join your neighbours for a morning litter-picking and recycling drive. Gloves and bags provided."));
            Add(new Event("Free Childhood Immunisation Drive", "Health", today.AddDays(3), "Sunnyside Clinic",
                "Walk-in vaccinations for children under 5, run by the Department of Health."));
            Add(new Event("Monthly Council Meeting", "Council", today.AddDays(5), "Civic Centre, Main Hall",
                "Open to the public. Agenda includes the Q3 infrastructure budget and ward reports."));
            Add(new Event("Youth Skills & Jobs Fair", "Youth", today.AddDays(6), "Community Hall, Extension 4",
                "Local employers and training providers offering CV workshops and on-the-spot interviews."));
            Add(new Event("Indigenous Tree Planting Day", "Environment", today.AddDays(7), "Riverside Green Belt",
                "Help plant 500 indigenous saplings along the river to combat erosion."));
            Add(new Event("Senior Citizens' Health Screening", "Health", today.AddDays(9), "Library Hall",
                "Free blood pressure, glucose and eye checks for residents over 60."));
            Add(new Event("Ward 12 Budget Imbizo", "Council", today.AddDays(10), "Thokoza Park",
                "Public participation session on how the ward's capital budget should be allocated next year."));
            Add(new Event("Youth Soccer Tournament Finals", "Sports & Recreation", today.AddDays(12), "Municipal Sports Grounds",
                "Under-17 league finals, with prize-giving by the mayor."));
            Add(new Event("Load-Shedding & Water Outage Briefing", "Utilities", today.AddDays(1), "Online / Community Radio",
                "Municipal engineers explain the maintenance schedule for the coming month."));
            Add(new Event("Small Business Support Workshop", "Economic Development", today.AddDays(14), "Civic Centre, Hall B",
                "Guidance on municipal supplier registration, tenders and SMME grant applications."));
            Add(new Event("Heritage Day Community Concert", "Arts & Culture", today.AddDays(16), "Thokoza Park Amphitheatre",
                "Celebrating local music, dance and food in honour of Heritage Day."));
            Add(new Event("Recycling & Waste Sorting Workshop", "Environment", today.AddDays(18), "Library Hall",
                "Practical session on the municipality's new separation-at-source recycling programme."));
            Add(new Event("Library Holiday Reading Programme", "Youth", today.AddDays(20), "Public Library",
                "Daily storytelling and reading activities for children during the school holidays."));
            Add(new Event("Road Maintenance Public Notice Session", "Utilities", today.AddDays(4), "Ward 12 Office",
                "Briefing on upcoming pothole repairs and temporary road closures."));
            Add(new Event("Fun Run for Clean Water", "Sports & Recreation", today.AddDays(21), "Riverside Green Belt",
                "5km and 10km community fun run raising awareness for water conservation."));
        }
    }
}
