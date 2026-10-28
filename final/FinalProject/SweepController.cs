namespace TransformerSweep;

//One measurement: the drive frequency and the peak-to-peak voltage on the secondary.
public record DataPoint(double FrequencyHz, double Vpp);

// Runs one sweep: for each frequency the strategy hands out, set the generator,
// wait for the reading to settle, read the scope. Always turns the output off afterward.

public class SweepController
{
    private readonly SignalGenerator _generator;
    private readonly Oscilloscope _scope;
    private readonly double _driveVpp;
    private readonly int _settleMs;

    public SweepController(SignalGenerator generator, Oscilloscope scope, double driveVpp, int settleMs)
    {
        _generator = generator;
        _scope = scope;
        _driveVpp = driveVpp;
        _settleMs = settleMs;
    }

    public List<DataPoint> Run(SweepStrategy sweep, CancellationToken token = default)
    {
        var points = new List<DataPoint>();
        bool firstPoint = true;

        try
        {
            while (sweep.HasMore && !token.IsCancellationRequested)
            {
                double hz = sweep.NextFrequency();
                _generator.SetSine(hz, _driveVpp);

                // Set the first frequency before turning the output on so it never starts at a leftover setting.
                if (firstPoint)
                {
                    _generator.OutputOn();
                    firstPoint = false;
                }

                // The scope's measurement averages recent acquisitions, so let the old frequency flush out.
                Thread.Sleep(_settleMs);

                points.Add(new DataPoint(hz, _scope.MeasurePeakToPeak()));
            }
        }
        finally
        {
            _generator.OutputOff();
        }

        return points;
    }
}