namespace TransformerSweep;

internal static class Program
{
    private const double StartHz = 1_000;
    private const double StopHz = 100_000;
    private const double DriveVpp = 1;   // drive level into the primary
    private const double MaxVpp = 5;     // generator safety ceiling
    private const int HardwareSettleMs = 300;

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

            using var generator = new SignalGenerator(genTransport, MaxVpp);
            using var scope = new Oscilloscope(scopeTransport);

            generator.Connect();
            scope.Connect();
            Console.WriteLine($"Generator: {generator.Identity}");
            Console.WriteLine($"Scope:     {scope.Identity}");

            var controller = new SweepController(generator, scope, DriveVpp, simulated ? 0 : HardwareSettleMs);
            List<DataPoint> points = controller.Run(new LinearSweep(StartHz, StopHz, 50));

            foreach (DataPoint p in points)
            {
                Console.WriteLine($"{p.FrequencyHz,12:F1} Hz  {p.Vpp,8:F4} Vpp");
            }

            DataPoint? best = Peak(points);
            Console.WriteLine(best is null
                ? "\nThe scope returned no valid readings."
                : $"\nHighest reading: {best.FrequencyHz:F1} Hz ({best.Vpp:F4} Vpp)");
            return 0;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error: {e.Message}");
            return 1;
        }
    }

    private static DataPoint? Peak(List<DataPoint> points) =>
        points.Where(p => !double.IsNaN(p.Vpp)).MaxBy(p => p.Vpp);
}