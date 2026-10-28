using System.Globalization;

namespace TransformerSweep;

/// Common behavior for bench equipment: owns a transport, connects, identifies
/// itself, and runs its own safe-shutdown step before the connection is dropped.

public abstract class Instrument : IDisposable
{
    private readonly Transport _transport;
    private bool _connected;

    protected Instrument(Transport transport)
    {
        _transport = transport;
    }

    public string Identity { get; private set; } = "(not connected)";

    public void Connect()
    {
        _transport.Open();
        _connected = true;
        Identity = Ask("*IDN?");
    }

    protected void Send(string command) => _transport.Write(command);

    protected string Ask(string command) => _transport.Query(command);

    // Leave the instrument safe before disconnecting. Default: nothing to do.
    protected virtual void Shutdown() { }

    public void Dispose()
    {
        try
        {
            if (_connected) Shutdown();
        }
        finally
        {
            _transport.Close();
        }
    }
}

public class SignalGenerator : Instrument
{
    private readonly double _maxVpp;
    private readonly int _channel;

    public SignalGenerator(Transport transport, double maxVpp, int channel = 1) : base(transport)
    {
        if (channel is < 1 or > 2) throw new ArgumentOutOfRangeException(nameof(channel), "The SDG1032X+ has channels 1-2.");
        _maxVpp = maxVpp;
        _channel = channel;
    }

    public void SetSine(double frequencyHz, double amplitudeVpp)
    {
        if (amplitudeVpp <= 0 || amplitudeVpp > _maxVpp)
            throw new ArgumentOutOfRangeException(nameof(amplitudeVpp), $"Must be above 0 and at most {_maxVpp} Vpp.");

        Send(FormattableString.Invariant($"C{_channel}:BSWV WVTP,SINE,FRQ,{frequencyHz:F3},AMP,{amplitudeVpp:F4},OFST,0"));
    }

    public void OutputOn() => Send($"C{_channel}:OUTP ON,LOAD,HZ");

    public void OutputOff() => Send($"C{_channel}:OUTP OFF");

    protected override void Shutdown() => OutputOff();
}

public class Oscilloscope : Instrument
{
    private readonly int _channel;

    public Oscilloscope(Transport transport, int channel = 2) : base(transport)
    {
        if (channel is < 1 or > 4) throw new ArgumentOutOfRangeException(nameof(channel), "The SDS824X HD has channels 1-4.");
        _channel = channel;
    }

    public double MeasurePeakToPeak()
    {
        string reply = Ask($"C{_channel}:PAVA? PKPK"); // e.g. "C2:PAVA PKPK,1.23E+00V"
        string number = reply.Split(',').Last().TrimEnd('V');

        return double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out double volts)
            ? volts
            : double.NaN;
    }
}