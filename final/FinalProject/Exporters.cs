namespace TransformerSweep;

// Something that can present a finished sweep
public abstract class ResultExporter
{
    public abstract void Export(List<DataPoint> points);
}

public class ConsoleReporter : ResultExporter
{
    public override void Export(List<DataPoint> points)
    {
        List<DataPoint> valid = points.Where(p => !double.IsNaN(p.Vpp)).OrderBy(p => p.FrequencyHz).ToList();
        double max = valid.Count > 0 ? valid.Max(p => p.Vpp) : 1;

        foreach (DataPoint p in valid)
        {
            string bar = new string('#', (int)(p.Vpp / max * 40));
            Console.WriteLine($"{p.FrequencyHz,12:F1} Hz  {p.Vpp,8:F4} Vpp  {bar}");
        }
    }
}

public class CsvExporter : ResultExporter
{
    private readonly string _path;

    public CsvExporter(string path)
    {
        _path = path;
    }

    public override void Export(List<DataPoint> points)
    {
        IEnumerable<string> rows = points
            .OrderBy(p => p.FrequencyHz)
            .Select(p => FormattableString.Invariant($"{p.FrequencyHz},{p.Vpp}"));

        File.WriteAllLines(_path, rows.Prepend("frequency_hz,vpp"));
        Console.WriteLine($"Saved {points.Count} points to {_path}");
    }
}