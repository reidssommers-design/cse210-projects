namespace TransformerSweep;

internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: dotnet run -- <instrument_ip>");
            return 2;
        }

        try
        {
            Transport transport = new TcpTransport(args[0]);
            transport.Open();
            Console.WriteLine(transport.Query("*IDN?"));
            transport.Close();
            return 0;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error: {e.Message}");
            return 1;
        }
    }
}