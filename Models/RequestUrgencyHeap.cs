using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    /// <summary>
    /// A binary max-heap keyed by integer urgency score (priority weight, with
    /// older requests breaking ties so nothing sits unattended indefinitely).
    /// This is what powers the admin dashboard's "Top Urgent Requests" panel:
    /// instead of sorting the entire request list every time it is displayed,
    /// the heap keeps the single most urgent request at the root in O(1)
    /// peek time and O(log n) to remove it, which is the textbook use case
    /// for a priority queue / heap over a general sort.
    /// </summary>
    public class RequestUrgencyHeap<TValue>
    {
        private readonly List<KeyValuePair<long, TValue>> _items = new List<KeyValuePair<long, TValue>>();
        public int Count
        {
            get { return _items.Count; }
        }
        public void Insert(long urgencyScore, TValue value)
        {
            _items.Add(new KeyValuePair<long, TValue>(urgencyScore, value));
            SiftUp(_items.Count - 1);
        }
        public TValue ExtractMax()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The urgency heap is empty.");
            }
            KeyValuePair<long, TValue> max = _items[0];
            int lastIndex = _items.Count - 1;
            _items[0] = _items[lastIndex];
            _items.RemoveAt(lastIndex);
            if (_items.Count > 0)
            {
                SiftDown(0);
            }
            return max.Value;
        }
        /// <summary>Non-destructive: returns the top N most urgent items without emptying the heap.</summary>
        public List<TValue> PeekTopN(int n)
        {
            List<KeyValuePair<long, TValue>> copy = new List<KeyValuePair<long, TValue>>(_items);
            List<TValue> result = new List<TValue>();
            RequestUrgencyHeap<TValue> scratch = new RequestUrgencyHeap<TValue>();
            scratch._items.AddRange(copy);
            int count = Math.Min(n, scratch.Count);
            for (int i = 0; i < count; i++)
            {
                result.Add(scratch.ExtractMax());
            }
            return result;
        }
        /// <summary>
        /// Non-destructive: returns up to <paramref name="n"/> of the most urgent
        /// items that satisfy <paramref name="predicate"/>, extracting from a copy of
        /// the heap lazily so it stops as soon as enough matches are found
        /// instead of ordering every item.
        /// </summary>
        public List<TValue> PeekTopMatching(Func<TValue, bool> predicate, int n)
        {
            RequestUrgencyHeap<TValue> scratch = new RequestUrgencyHeap<TValue>();
            scratch._items.AddRange(_items);
            List<TValue> result = new List<TValue>();
            while (result.Count < n && scratch.Count > 0)
            {
                TValue candidate = scratch.ExtractMax();
                if (predicate(candidate))
                {
                    result.Add(candidate);
                }
            }
            return result;
        }
        private void SiftUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_items[index].Key <= _items[parent].Key) break;
                Swap(index, parent);
                index = parent;
            }
        }
        private void SiftDown(int index)
        {
            int count = _items.Count;
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int largest = index;
                if (left < count && _items[left].Key > _items[largest].Key) largest = left;
                if (right < count && _items[right].Key > _items[largest].Key) largest = right;
                if (largest == index) break;
                Swap(index, largest);
                index = largest;
            }
        }
        private void Swap(int a, int b)
        {
            KeyValuePair<long, TValue> temp = _items[a];
            _items[a] = _items[b];
            _items[b] = temp;
        }
    }
}
