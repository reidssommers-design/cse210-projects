namespace TransformerSweep;

/// Hands out frequencies one at a time. The base class does the counting;
/// subclasses only say where point number i lands.

public abstract class SweepStrategy
{
    private int _index;

    protected SweepStrategy(double startHz, double stopHz, int count)
    {
        if (startHz <= 0 || stopHz <= startHz) throw new ArgumentException("Need 0 < start < stop.");
        if (count < 2) throw new ArgumentException("Need at least 2 points.");

        StartHz = startHz;
        StopHz = stopHz;
        Count = count;
    }

    protected double StartHz { get; }

    protected double StopHz { get; }

    protected int Count { get; }

    public bool HasMore => _index < Count;

    public double NextFrequency()
    {
        if (!HasMore) throw new InvalidOperationException("Sweep is finished.");
        return FrequencyAt(_index++);
    }
    //Frequency of point <paramref name="index"/>; 0 is the start and Count-1 is the stop
    protected abstract double FrequencyAt(int index);
}

//Evenly spaced in Hz. Good for zooming in once you know roughly where the peak is
public class LinearSweep : SweepStrategy
{
    public LinearSweep(double startHz, double stopHz, int count) : base(startHz, stopHz, count) { }

    protected override double FrequencyAt(int index) =>
        StartHz + (StopHz - StartHz) * index / (Count - 1);
}