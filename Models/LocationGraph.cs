using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
   
    public class GraphEdge
    {
        public string From;
        public string To;
        public int DistanceKm;
        public GraphEdge(string from, string to, int distanceKm)
        {
            From = from;
            To = to;
            DistanceKm = distanceKm;
        }
    }
    public static class LocationGraph
    {
        private static readonly Dictionary<string, List<GraphEdge>> _adjacency =
            new Dictionary<string, List<GraphEdge>>(StringComparer.OrdinalIgnoreCase);
        public static readonly string[] Wards =
        {
         "Ward 1 - Central CBD",
         "Ward 2 - Riverside",
         "Ward 3 - Hillview",
         "Ward 4 - Greenfields",
         "Ward 5 - Oakdale",
         "Ward 6 - Sunnyside",
         "Ward 7 - Lakeview",
         "Ward 8 - Industria"
     };
        static LocationGraph()
        {
            foreach (string ward in Wards)
            {
                _adjacency[ward] = new List<GraphEdge>();
            }
            // A simplified municipal road network connecting the wards above.
            AddEdge("Ward 1 - Central CBD", "Ward 2 - Riverside", 4);
            AddEdge("Ward 1 - Central CBD", "Ward 3 - Hillview", 6);
            AddEdge("Ward 1 - Central CBD", "Ward 6 - Sunnyside", 3);
            AddEdge("Ward 2 - Riverside", "Ward 4 - Greenfields", 5);
            AddEdge("Ward 2 - Riverside", "Ward 7 - Lakeview", 7);
            AddEdge("Ward 3 - Hillview", "Ward 5 - Oakdale", 4);
            AddEdge("Ward 3 - Hillview", "Ward 6 - Sunnyside", 5);
            AddEdge("Ward 4 - Greenfields", "Ward 5 - Oakdale", 6);
            AddEdge("Ward 4 - Greenfields", "Ward 8 - Industria", 8);
            AddEdge("Ward 5 - Oakdale", "Ward 7 - Lakeview", 3);
            AddEdge("Ward 6 - Sunnyside", "Ward 8 - Industria", 9);
            AddEdge("Ward 7 - Lakeview", "Ward 8 - Industria", 4);
        }
        private static void AddEdge(string a, string b, int distanceKm)
        {
            _adjacency[a].Add(new GraphEdge(a, b, distanceKm));
            _adjacency[b].Add(new GraphEdge(b, a, distanceKm));
        }
        /// <summary>Breadth-first traversal, returning wards in visiting order starting from <paramref name="start"/>.</summary>
        public static List<string> BreadthFirstSearch(string start)
        {
            List<string> order = new List<string>();
            if (!_adjacency.ContainsKey(start)) return order;
            HashSet<string> visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(start);
            visited.Add(start);
            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                order.Add(current);
                foreach (GraphEdge edge in _adjacency[current].OrderBy(e => e.DistanceKm))
                {
                    if (!visited.Contains(edge.To))
                    {
                        visited.Add(edge.To);
                        queue.Enqueue(edge.To);
                    }
                }
            }
            return order;
        }
        /// <summary>Depth-first traversal, returning wards in visiting order starting from <paramref name="start"/>.</summary>
        public static List<string> DepthFirstSearch(string start)
        {
            List<string> order = new List<string>();
            if (!_adjacency.ContainsKey(start)) return order;
            HashSet<string> visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            DfsVisit(start, visited, order);
            return order;
        }
        private static void DfsVisit(string current, HashSet<string> visited, List<string> order)
        {
            visited.Add(current);
            order.Add(current);
            foreach (GraphEdge edge in _adjacency[current].OrderBy(e => e.DistanceKm))
            {
                if (!visited.Contains(edge.To))
                {
                    DfsVisit(edge.To, visited, order);
                }
            }
        }
        /// <summary>
        /// Returns the wards reachable within <paramref name="maxHops"/> of
        /// <paramref name="start"/>, using the BFS order above cut off by
        /// distance-from-start - used for "nearby open requests".
        /// </summary>
        public static List<string> WardsWithinHops(string start, int maxHops)
        {
            List<string> result = new List<string>();
            if (!_adjacency.ContainsKey(start)) return result;
            Dictionary<string, int> hopsFromStart = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            Queue<string> queue = new Queue<string>();
            hopsFromStart[start] = 0;
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                int currentHops = hopsFromStart[current];
                if (currentHops >= maxHops) continue;
                foreach (GraphEdge edge in _adjacency[current])
                {
                    if (!hopsFromStart.ContainsKey(edge.To))
                    {
                        hopsFromStart[edge.To] = currentHops + 1;
                        queue.Enqueue(edge.To);
                    }
                }
            }
            result.AddRange(hopsFromStart.Keys.Where(w => !string.Equals(w, start, StringComparison.OrdinalIgnoreCase)));
            return result;
        }
        /// <summary>
        /// Prim's algorithm, restricted to the subset of wards supplied
        /// (typically "every ward with at least one open request"), returning
        /// the minimum set of roads that connects all of them and the total
        /// distance. If fewer than two wards are supplied, an empty route is
        /// returned since there is nothing to connect.
        /// </summary>
        public static MstResult MinimumSpanningTree(List<string> requiredWards)
        {
            List<GraphEdge> mstEdges = new List<GraphEdge>();
            int totalDistance = 0;
            List<string> wards = requiredWards
                .Where(w => _adjacency.ContainsKey(w))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            if (wards.Count < 2)
            {
                return new MstResult(mstEdges, 0);
            }
            HashSet<string> inTree = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            inTree.Add(wards[0]);
            while (inTree.Count < wards.Count)
            {
                GraphEdge bestEdge = null;
                // Find the cheapest edge connecting the tree so far to a
                // required ward that is not yet in the tree. Edges may hop
                // through intermediate (non-required) wards via shortest path.
                foreach (string treeWard in inTree)
                {
                    foreach (string candidate in wards.Where(w => !inTree.Contains(w)))
                    {
                        int distance = ShortestPathDistance(treeWard, candidate);
                        if (distance == int.MaxValue) continue;
                        if (bestEdge == null || distance < bestEdge.DistanceKm)
                        {
                            bestEdge = new GraphEdge(treeWard, candidate, distance);
                        }
                    }
                }
                if (bestEdge == null) break; // Remaining wards are unreachable.
                mstEdges.Add(bestEdge);
                totalDistance += bestEdge.DistanceKm;
                inTree.Add(bestEdge.To);
            }
            return new MstResult(mstEdges, totalDistance);
        }
        /// <summary>A single frontier entry used by the shortest-path search below.</summary>
        private class FrontierEntry : IComparable<FrontierEntry>
        {
            public int Distance;
            public string Ward;
            public FrontierEntry(int distance, string ward)
            {
                Distance = distance;
                Ward = ward;
            }
            public int CompareTo(FrontierEntry other)
            {
                int cmp = Distance.CompareTo(other.Distance);
                if (cmp != 0) return cmp;
                return string.Compare(Ward, other.Ward, StringComparison.OrdinalIgnoreCase);
            }
        }
        /// <summary>Dijkstra-style shortest path distance between two wards through the full road network.</summary>
        private static int ShortestPathDistance(string from, string to)
        {
            if (string.Equals(from, to, StringComparison.OrdinalIgnoreCase)) return 0;
            Dictionary<string, int> distances = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (string ward in _adjacency.Keys) distances[ward] = int.MaxValue;
            distances[from] = 0;
            HashSet<string> visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<FrontierEntry> frontier = new SortedSet<FrontierEntry>();
            frontier.Add(new FrontierEntry(0, from));
            while (frontier.Count > 0)
            {
                FrontierEntry entry = frontier.Min;
                frontier.Remove(entry);
                if (!visited.Add(entry.Ward)) continue;
                if (string.Equals(entry.Ward, to, StringComparison.OrdinalIgnoreCase)) return entry.Distance;
                foreach (GraphEdge edge in _adjacency[entry.Ward])
                {
                    int newDist = entry.Distance + edge.DistanceKm;
                    if (newDist < distances[edge.To])
                    {
                        distances[edge.To] = newDist;
                        frontier.Add(new FrontierEntry(newDist, edge.To));
                    }
                }
            }
            return distances.ContainsKey(to) ? distances[to] : int.MaxValue;
        }
    }
    /// <summary>Result of a Minimum Spanning Tree computation: the chosen edges and their total distance.</summary>
    public class MstResult
    {
        public List<GraphEdge> Edges { get; private set; }
        public int TotalDistanceKm { get; private set; }
        public MstResult(List<GraphEdge> edges, int totalDistanceKm)
        {
            Edges = edges;
            TotalDistanceKm = totalDistanceKm;
        }
    }
}
