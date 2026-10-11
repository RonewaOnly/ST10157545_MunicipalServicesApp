using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    public class EventPriorityQueue
    {
        private readonly List<KeyValuePair<DateTime, Event>> _heap = new List<KeyValuePair<DateTime, Event>>();
        public int Count
        {
            get { return _heap.Count; }
        }
        public void Enqueue(Event item, DateTime priority)
        {
            _heap.Add(new KeyValuePair<DateTime, Event>(priority, item));
            int i = _heap.Count - 1;
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (_heap[parent].Key.CompareTo(_heap[i].Key) <= 0)
                {
                    break;
                }
                Swap(parent, i);
                i = parent;
            }
        }
        public Event Dequeue()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }
            Event result = _heap[0].Value;
            int last = _heap.Count - 1;
            _heap[0] = _heap[last];
            _heap.RemoveAt(last);
            int i = 0;
            while (true)
            {
                int left = (2 * i) + 1;
                int right = (2 * i) + 2;
                int smallest = i;
                if (left < _heap.Count && _heap[left].Key.CompareTo(_heap[smallest].Key) < 0)
                {
                    smallest = left;
                }
                if (right < _heap.Count && _heap[right].Key.CompareTo(_heap[smallest].Key) < 0)
                {
                    smallest = right;
                }
                if (smallest == i)
                {
                    break;
                }
                Swap(i, smallest);
                i = smallest;
            }
            return result;
        }
        /// <summary>
        /// Returns every item currently in the queue in priority order
        /// (soonest date first) without removing them - used to display the
        /// full "upcoming events" list while leaving the underlying heap intact.
        /// </summary>
        public List<Event> ToOrderedList()
        {
            List<Event> items = new List<Event>();
            foreach (KeyValuePair<DateTime, Event> entry in _heap)
            {
                items.Add(entry.Value);
            }
            items.Sort((a, b) => a.Date.CompareTo(b.Date));
            return items;
        }
        private void Swap(int a, int b)
        {
            KeyValuePair<DateTime, Event> temp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = temp;
        }
    }
}
