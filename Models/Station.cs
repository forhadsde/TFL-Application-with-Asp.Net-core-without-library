public class Station
{
    public string Name { get; set; }
    public string Zone { get; set; }
    public CustomList<Edge> Edges { get; set; }
    public CustomList<Line> Lines { get; set; }

    public Station(string name)
    {
        Name = name;
        Edges = new CustomList<Edge>();
        Lines = new CustomList<Line>();
    }

    public void AddEdge(Edge edge)
    {
        if (!Edges.Contains(edge))
        {
            Edges.Add(edge);
        }
    }

    public void AddLine(Line line)
    {
        if (!Lines.Contains(line))
        {
            Lines.Add(line);
        }
    }
}
