using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using NLog;

using TCPRecorder.Client;
using TCPRecorder.Client.ConfigurationFile;
using TCPRecorder.Client.Extensions;
using TCPRecorder.Client.Packet;
using TCPRecorder.Client.Parameters;
using TCPRecorder.Client.Utilities;

namespace Sample
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            using var cts = new CancellationTokenSource();

            Do(cts);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);

            if (!cts.IsCancellationRequested)
            {
                cts.Cancel();
            }
        }

        private static async void Do(CancellationTokenSource cts)
        {
            try
            {
                await DoAsync(cts.Token);
            }
            catch (TaskCanceledException)
            {
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Console.WriteLine(ex);
            }
        }

        private static async Task DoAsync(CancellationToken token)
        {
            // TCP listener recorder settings
            var ipAddress = IPAddress.Loopback;
            const int port = 4567;

            // Configuration settings
            var configurationIdentifier = "SampleConfigurationName";
            const int configurationVersion = 1; // Bump this whenever parameter definitions are changed
            const string applicationGroupName = "SampleParameters"; // Top-level application name (group parameters are added too)
            const double frequencyInHz = 100;

            // Parameters (the packet data must match with no padding)
            var sineWaveParameter = new PacketParameter("SineWave", "Sample Sine Wave", PacketFieldType.Double, (-100, 100), format: "%.3f");
            var cosineWaveParameter = new PacketParameter("CosineWave", "Sample Cosine Wave", PacketFieldType.Double, (-100, 100), format: "%.3f");
            var tanWaveParameter = new PacketParameter("TanWave", "Sample Tan Wave", PacketFieldType.Double, (-100, 100), format: "%.3f");
            var randomParameter = new PacketParameter("Random", "Sample Random", PacketFieldType.Double, (0, 1));

            // Generate configuration file
            var packetSize = GenerateConfiguration(
                ref configurationIdentifier,
                configurationVersion,
                applicationGroupName,
                frequencyInHz,
                sineWaveParameter,
                cosineWaveParameter,
                tanWaveParameter,
                randomParameter);

            using var client = new Client();

            await client.Connect(ipAddress, port).ConfigureAwait(false);
            if (!client.IsConnected)
            {
                throw new InvalidOperationException("Connection failed!");
            }

            if (!await client.SendConfigurationIdentifier(configurationIdentifier, token).ConfigureAwait(false))
            {
                throw new InvalidOperationException("Sending configuration failed!");
            }

            // Start time in whole seconds (trying to avoid misaligned timestamps, as ATLAS detects and reports every instance)
            var startTimeInS = Math.Floor(TimeUtilities.GetNowSinceMidnightInSeconds());

            // Send start lap packet
            var lapNumber = (short)1;
            await client.SendLap(startTimeInS, lapNumber, (byte)TriggerSource.Default, $"Lap {lapNumber++}", true, token).ConfigureAwait(false);
            var nextLapTimeInS = startTimeInS + 60;

            // Data settings
            var nextDataTimeInS = startTimeInS;
            var intervalInNs = TimeUtilities.IntervalInNanoseconds(frequencyInHz);
            var offsetInNs = new NanoSeconds(0);
            var angleIntervalInRads = (2 * Math.PI) / (10 * frequencyInHz); // 10s for 360 deg
            var angleInRads = 0.0;

            // Reuse buffer...
            var buffer = new byte[packetSize];

            var random = new Random(-1);
            while (!token.IsCancellationRequested)
            {
                var now = TimeUtilities.GetNowSinceMidnightInSeconds();
                while (nextDataTimeInS < now)
                {
                    // Set parameter values at the appropriate offsets within the buffer
                    // The buffer is laid out in the order specified in the configuration
                    // The buffer contents can be set however is appropriate, just using simple helpers here
                    sineWaveParameter.SetValue(buffer, Math.Sin(angleInRads) * 100);
                    cosineWaveParameter.SetValue(buffer, Math.Cos(angleInRads) * 100);
                    tanWaveParameter.SetValue(buffer, Math.Tan(angleInRads) * 100);
                    angleInRads += angleIntervalInRads;

                    randomParameter.SetValue(buffer, random.NextDouble());

                    // Send updated data packet
                    await client.SendData(nextDataTimeInS, token, buffer).ConfigureAwait(false);

                    // Ensure each packet has consecutive aligned timestamps
                    offsetInNs += intervalInNs;
                    nextDataTimeInS = startTimeInS + offsetInNs.Seconds;

                    if (nextLapTimeInS < now)
                    {
                        // Send lap packet
                        await client.SendLap(nextLapTimeInS, lapNumber, (byte)TriggerSource.Default, $"Lap {lapNumber++}", true, token).ConfigureAwait(false);
                        nextLapTimeInS += 60 + (-15 + random.NextDouble() * 30); // Random lap duration
                    }
                }

                // No need to send packets at the same frequency
                await Task.Delay(500 + random.Next(500), token).ConfigureAwait(false);
            }
        }

        private static int GenerateConfiguration(
            ref string configurationIdentifier,
            int configurationVersion,
            string applicationGroupName,
            double frequencyInHz,
            params PacketParameter[] packetParameters)
        {
            if (!PacketConfiguration.GenerateConfiguration(
                ref configurationIdentifier,
                configurationVersion,
                applicationGroupName,
                packetParameters,
                out var packetSize,
                frequencyInHz))
            {
                throw new InvalidOperationException("Generate Configuration failed!");
            }

            return packetSize;
        }
    }
}
