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