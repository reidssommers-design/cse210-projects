using System.Globalization;

namespace TransformerSweep;

internal static class Program
{
    private const int HardwareSettleMs = 300;

    // Usage:  dotnet run                                       (simulated bench)
    //         dotnet run -- --gen <sdg_ip> --scope <sds_ip>    (real hardware)
    //         optional: --start <Hz> --stop <Hz> --amp <Vpp> --max-amp <Vpp> --gen-ch <1-2> --scope-ch <1-4>
    //                   --gen-ch <1-2> --scope-ch <1-4>
    // Ctrl+C once: stop the sweep and switch the generator output off. Twice: force quit.
    private static int Main(string[] args)
    {
        string? genIp = Option(args, "--gen");
        string? scopeIp = Option(args, "--scope");
        if ((genIp is null) != (scopeIp is null))
        {
            Console.Error.WriteLine("Give both --gen and --scope for real hardware, or neither for simulation.");
            return 2;
        }
        bool simulated = genIp is null;

        double startHz = Number(args, "--start", 1_000);
        double stopHz = Number(args, "--stop", 100_000);
        double driveVpp = Number(args, "--amp", 1);
        double maxVpp = Number(args, "--max-amp", 5);
        int genChannel = Integer(args, "--gen-ch", 1);
        int scopeChannel = Integer(args, "--scope-ch", 2);

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (sender, e) =>
        {
            if (cts.IsCancellationRequested) return;   // second Ctrl+C: let it kill the process
            e.Cancel = true;                           // first Ctrl+C: don't kill us, unwind cleanly
            cts.Cancel();
        };

        try
        {
            Transport genTransport, scopeTransport;
            if (simulated)
            {
                var bench = new SimulatedBench();
                genTransport = new SimulatedTransport(bench, "SIMULATED,SDG1032X+");
                scopeTransport = new SimulatedTransport(bench, "SIMULATED,SDS824X HD");
                Console.WriteLine("Simulated bench (resonance at 12.5 kHz). Pass --gen <ip> --scope <ip> for real hardware.");
            }
            else
            {
                genTransport = new TcpTransport(genIp!);
                scopeTransport = new TcpTransport(scopeIp!);
            }

            using var generator = new SignalGenerator(genTransport, maxVpp, genChannel);
            using var scope = new Oscilloscope(scopeTransport, scopeChannel);

            generator.Connect();
            scope.Connect();
            Console.WriteLine($"Generator: {generator.Identity}");
            Console.WriteLine($"Scope:     {scope.Identity}");

            var controller = new SweepController(generator, scope, driveVpp, simulated ? 0 : HardwareSettleMs);

            // Pass 1: coarse log sweep across the whole range.
            List<DataPoint> points = controller.Run(new LogSweep(startHz, stopHz, 40), cts.Token);

            if (cts.IsCancellationRequested)
            {
                Console.Error.WriteLine("Cancelled. Generator output is off.");
                return 130;
            }

            DataPoint? roughPeak = Peak(points);
            if (roughPeak is null)
            {
                Console.Error.WriteLine("The scope returned no valid readings.");
                return 1;
            }

            // Pass 2: fine linear sweep around the rough peak.
            points.AddRange(controller.Run(new LinearSweep(roughPeak.FrequencyHz * 0.8, roughPeak.FrequencyHz * 1.2, 40), cts.Token));

            ResultExporter[] exporters = { new ConsoleReporter(), new CsvExporter("sweep.csv") };
            foreach (ResultExporter exporter in exporters)
            {
                exporter.Export(points);
            }

            DataPoint best = Peak(points)!;
            Console.WriteLine($"\nResonant frequency: {best.FrequencyHz:F1} Hz ({best.Vpp:F4} Vpp)");
            return 0;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error: {e.Message}");
            return 1;
        }
    }

    // The token right after <paramref name="name"/>, or null if the flag isn't there
    private static string? Option(string[] args, string name)
    {
        int i = Array.IndexOf(args, name);
        return i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
    }

    private static double Number(string[] args, string name, double fallback) =>
        double.TryParse(Option(args, name), NumberStyles.Float, CultureInfo.InvariantCulture, out double v) ? v : fallback;