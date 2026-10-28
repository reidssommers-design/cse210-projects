using System.Globalization;
using System.Net.Sockets;
using System.Text;

// Instruments will use this class to send and recieve data
// Instruments can only call Write and Query

public abstract class Transport
{
    public abstract void Open();

    /// Send a command that produces no reply.
    public abstract void Write(string command);
    /// Send a command and return the one-line reply.
    public abstract string Query(string command);
    public abstract void Close();
}