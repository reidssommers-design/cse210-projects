using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace TransformerSweep;

public abstract class Transport
{
    public abstract void Open();
    
    // Send a command that produces no reply
    public abstract void Write(string command);

    // Send a command and return the one-line reply
    public abstract string Query(string command);

    public abstract void Close();
}

//Raw-socket SCPI. Siglent instruments listen on TCP port 5025 and expect newline-terminated commands
public class TcpTransport : Transport
{
    private const int TimeoutMs = 5000;

    private readonly string _host;
    private readonly int _port;
    private TcpClient? _client;
    private StreamReader? _reader;
    private StreamWriter? _writer;

    public TcpTransport(string host, int port = 5025)
    {
        _host = host;
        _port = port;
    }

    public override void Open()
    {
        _client = new TcpClient { ReceiveTimeout = TimeoutMs, SendTimeout = TimeoutMs };
        _client.Connect(_host, _port);

        NetworkStream stream = _client.GetStream();
        _reader = new StreamReader(stream, Encoding.ASCII);
        _writer = new StreamWriter(stream, Encoding.ASCII) { NewLine = "\n", AutoFlush = true };
    }

    public override void Write(string command)
    {
        if (_writer is null) throw new InvalidOperationException("Transport is not open.");
        _writer.WriteLine(command);
    }

    public override string Query(string command)
    {
        if (_reader is null) throw new InvalidOperationException("Transport is not open.");
        Write(command);
        return _reader.ReadLine() ?? throw new IOException("Instrument closed the connection.");
    }

    public override void Close()
    {
        _writer?.Dispose();
        _reader?.Dispose();
        _client?.Dispose();
        _writer = null;
        _reader = null;
        _client = null;
    }
}


// A pretend bench: the generator's frequency/amplitude feed a band-pass style
// resonance (12.5 kHz, Q of 6) that the "scope" reads back. Both simulated
// transports share one bench so a change made through one is seen by the other.

public class SimulatedBench
{
    private const double ResonanceHz = 12_500;
    private const double QualityFactor = 6;
    private const double PeakGain = 0.8;

    private double _frequencyHz;
    private double _amplitudeVpp;
    private bool _outputOn;

    public void Configure(double frequencyHz, double amplitudeVpp)
    {
        _frequencyHz = frequencyHz;
        _amplitudeVpp = amplitudeVpp;
    }

    public void SetOutput(bool on) => _outputOn = on;

    public double MeasureVpp()
    {
        if (!_outputOn) return 0.002; // noise floor

        double ratio = _frequencyHz / ResonanceHz;
        double detune = ratio - 1.0 / ratio;
        return _amplitudeVpp * PeakGain / Math.Sqrt(1 + QualityFactor * QualityFactor * detune * detune);
    }
}

public class SimulatedTransport : Transport
{
    private readonly SimulatedBench _bench;
    private readonly string _identity;

    public SimulatedTransport(SimulatedBench bench, string identity)
    {
        _bench = bench;
        _identity = identity;
    }

    public override void Open() { }

    public override void Close() { }

    public override void Write(string command) => Handle(command);

    public override string Query(string command) =>
        Handle(command) ?? throw new InvalidOperationException($"Simulator has no reply for: {command}");

    private string? Handle(string command)
    {
        if (command == "*IDN?") return _identity;

        if (command.Contains(":BSWV "))
        {
            // e.g. "C1:BSWV WVTP,SINE,FRQ,1000.000,AMP,1.0000,OFST,0"
            string[] parts = command.Split(',');
            _bench.Configure(FieldAfter(parts, "FRQ"), FieldAfter(parts, "AMP"));
        }
        else if (command.Contains(":OUTP ON")) _bench.SetOutput(true);
        else if (command.Contains(":OUTP OFF")) _bench.SetOutput(false);
        else if (command.Contains(":PAVA? PKPK"))
        {
            return FormattableString.Invariant($"C2:PAVA PKPK,{_bench.MeasureVpp():E5}V");
        }

        return null;
    }

    private static double FieldAfter(string[] parts, string name) =>
        double.Parse(parts[Array.IndexOf(parts, name) + 1], CultureInfo.InvariantCulture);
}