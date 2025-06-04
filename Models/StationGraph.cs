using metromap.Models;

public class StationGraph
{
    public CustomList<Station> Stations { get; set; }
    public CustomList<Line> Lines { get; set; }
    public CustomList<Edge> Edges { get; set; }

    public StationGraph()
    {
        Stations = new CustomList<Station>();
        Lines = new CustomList<Line>();
        Edges = new CustomList<Edge>();
    }

    public bool AddStation(Station station)
    {
        if (Stations.Any(s => s.Name == station.Name))
        {
            return false;
        }
        Stations.Add(station);
        return true;
    }

    public Station GetStation(string name)
    {
        return Stations.FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
    }

    public Edge GetEdge(Station from, Station to)
    {
        return from.Edges.FirstOrDefault(e => e.To == to);
    }

    public void AddLine(Line line)
    {
        if (Lines.Any(l => l.Name == line.Name && l.Direction == line.Direction))
        {
            return;
        }
        Lines.Add(line);
    }

    public void AddEdge(Edge edge)
    {
        if (Edges.Any(e => e.From == edge.From && e.To == edge.To))
        {
            return;
        }
        Edges.Add(edge);
    }

    public (CustomList<(Station, Line)> path, double time) FindShortestPath(Station startStation, Station endStation)
    {
        CustomDictionary<Station, (double distance, Station previous, Line line)> dist = new CustomDictionary<Station, (double, Station, Line)>();
        foreach (var station in Stations)
        {
            dist[station] = (double.MaxValue, null, null);
        }
        dist[startStation] = (0, null, null);

        var priorityQueue = new CustomPriorityQueue<Station, double>();
        priorityQueue.Enqueue(startStation, 0);

        while (priorityQueue.Count > 0)
        {
            var current = priorityQueue.Dequeue();
            if (current == endStation) break;

            Line currentLine = dist[current].line;

            foreach (var edge in current.Edges)
            {
                if (edge.IsClosed) continue;

                var neighbor = edge.To;
                double additionalTime = edge.GetTime();
                if (currentLine != null && edge.Line != currentLine)
                {
                    additionalTime += 2; // Adding transfer penalty
                }

                var newDistance = dist[current].distance + additionalTime;
                if (newDistance < dist[neighbor].distance)
                {
                    dist[neighbor] = (newDistance, current, edge.Line);
                    priorityQueue.Enqueue(neighbor, newDistance);
                }
            }
        }

        if (dist[endStation].previous == null) return (new CustomList<(Station, Line)>(), 0); // No path found

        CustomList<(Station, Line)> path = new CustomList<(Station, Line)>();
        for (var step = endStation; step != startStation; step = dist[step].previous)
        {
            path.Add((step, dist[step].line));
        }
        path.Add((startStation, null)); // Add the start station with no associated line
        path.Reverse();

        return (path, dist[endStation].distance);
    }
}
