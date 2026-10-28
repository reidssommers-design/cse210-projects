namespace TransformerSweep;

internal static class Program
{
    // FIRST manual measurement! drive 1 kHz into the primary, read the secondary.
    //   d otnet run -- <sdg_ip> <sds_ip>
    private static int Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: dotnet run -- <sdg_ip> <sds_ip>");
            return 2;
        }

        try
        {
            using var generator = new SignalGenerator(new TcpTransport(args[0]), 5);
            using var scope = new Oscilloscope(new TcpTransport(args[1]));

            generator.Connect();
            scope.Connect();
            Console.WriteLine($"Generator: {generator.Identity}");
            Console.WriteLine($"Scope:     {scope.Identity}");

            generator.SetSine(1_000, 1);
            generator.OutputOn();
            Thread.Sleep(300);
            Console.WriteLine($"Secondary at 1 kHz: {scope.MeasurePeakToPeak():F4} Vpp");
            return 0;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error: {e.Message}");
            return 1;
        }
    }
}