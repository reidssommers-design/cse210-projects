using System.Globalization;

namespace TransformerSweep;

/// <summary>
/// Common behavior for bench equipment: owns a transport, connects, identifies
/// itself, and runs its own safe-shutdown step before the connection is dropped.
/// </summary>
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

    /// <summary>Leave the instrument safe before disconnecting. Default: nothing to do.</summary>
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

/// <summary>Siglent SDG1032X+, channel 1. The amplitude ceiling is enforced here so no caller can exceed it.</summary>
public class SignalGenerator : Instrument
{
    private readonly double _maxVpp;

    public SignalGenerator(Transport transport, double maxVpp) : base(transport)
    {
        _maxVpp = maxVpp;
    }

    public void SetSine(double frequencyHz, double amplitudeVpp)
    {
        if (amplitudeVpp <= 0 || amplitudeVpp > _maxVpp)
            throw new ArgumentOutOfRangeException(nameof(amplitudeVpp), $"Must be above 0 and at most {_maxVpp} Vpp.");

        Send(FormattableString.Invariant($"C1:BSWV WVTP,SINE,FRQ,{frequencyHz:F3},AMP,{amplitudeVpp:F4},OFST,0"));
    }

    /// <summary>LOAD,HZ = high impedance, so the amplitude shown matches what a transformer primary actually sees.</summary>
    public void OutputOn() => Send("C1:OUTP ON,LOAD,HZ");

    public void OutputOff() => Send("C1:OUTP OFF");

    protected override void Shutdown() => OutputOff();
}

/// <summary>Siglent SDS824X HD, channel 2 (the secondary coil). Reads peak-to-peak voltage.</summary>
public class Oscilloscope : Instrument
{
    public Oscilloscope(Transport transport) : base(transport) { }

    /// <summary>Peak-to-peak volts, or NaN if the scope can't give a valid reading (it replies with asterisks).</summary>
    public double MeasurePeakToPeak()
    {
        string reply = Ask("C2:PAVA? PKPK"); // e.g. "C2:PAVA PKPK,1.23E+00V"
        string number = reply.Split(',').Last().TrimEnd('V');

        return double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out double volts)
            ? volts
            : double.NaN;
    }
}