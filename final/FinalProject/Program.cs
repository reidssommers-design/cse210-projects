namespace TransformerSweep;

internal static class Program
{
    // Usage:  dotnet run                          (simulated bench)
    //         dotnet run -- <10.10.10.4 <10.10.10.3>     (real hardware)
    private static int Main(string[] args)
    {
        bool simulated = args.Length != 2;

        try
        {
            Transport genTransport, scopeTransport;
            if (simulated)
            {
                var bench = new SimulatedBench();
                genTransport = new SimulatedTransport(bench, "SIMULATED,SDG1032X+");
                scopeTransport = new SimulatedTransport(bench, "SIMULATED,SDS824X HD");
                Console.WriteLine("Simulated bench (resonance at 12.5 kHz). Pass <sdg_ip> <sds_ip> for real hardware.");
            }
            else
            {
                genTransport = new TcpTransport(args[0]);
                scopeTransport = new TcpTransport(args[1]);
            }

            using var generator = new SignalGenerator(genTransport, 5);
            using var scope = new Oscilloscope(scopeTransport);

            generator.Connect();
            scope.Connect();
            Console.WriteLine($"Generator: {generator.Identity}");
            Console.WriteLine($"Scope:     {scope.Identity}");

            generator.SetSine(12_500, 1);
            generator.OutputOn();
            Thread.Sleep(simulated ? 0 : 300);
            Console.WriteLine($"Secondary at 12.5 kHz: {scope.MeasurePeakToPeak():F4} Vpp");
            return 0;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error: {e.Message}");
            return 1;
        }
    }
}