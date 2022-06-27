// <copyright file="Client.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using TCPRecorder.Client.Utilities;

namespace TCPRecorder.Client
{
    public sealed class Client : IDisposable
    {
        private const ushort MessageProtocolVersion = 2;
        private static readonly Logger NLogLogger = LogManager.GetCurrentClassLogger();
        private readonly int sendBufferSize;
        private readonly TcpClient tcpClient = new TcpClient();
        private NetworkStream stream;

        public Client(int sendBufferSize = 0)
        {
            this.sendBufferSize = sendBufferSize;
        }

        public bool IsConnected { get; private set; }

        public void Dispose()
        {
            this.tcpClient.Dispose();
        }

        public async Task Connect(IPAddress ipAddress, int port)
        {
            try
            {
                await this.tcpClient.ConnectAsync(ipAddress, port).ConfigureAwait(false);

                // Optimize TCP settings
                this.tcpClient.NoDelay = true;
                if (this.sendBufferSize > 0)
                {
                    this.tcpClient.SendBufferSize = this.sendBufferSize;
                }

                this.stream = this.tcpClient.GetStream();
                this.IsConnected = true;
            }
            catch (Exception ex)
            {
                NLogLogger.Error(ex, "Failed to connect to TCP recorder");
            }
        }

        public async Task<bool> SendConfigurationIdentifier(string configurationIdentifier, CancellationToken token)
        {
            var configurationIdentifierBytes = Encoding.ASCII.GetBytes(configurationIdentifier);

            var timestamp = TimeUtilities.GetNowSinceMidnightInSeconds();
            if (!await this.SendHeader(MessageType.Config, timestamp, configurationIdentifierBytes.Length, token).ConfigureAwait(false))
            {
                return false;
            }

            try
            {
                await this.stream.WriteAsync(configurationIdentifierBytes, 0, configurationIdentifierBytes.Length, token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                NLogLogger.Error(ex, "Failed to write configuration identifier string to TCP recorder");
                return false;
            }

            return true;
        }

        public async Task<bool> SendData(double timestamp, CancellationToken token, params byte[][] buffers)
        {
            var dataSize = buffers.Sum(b => b.Length);
            if (!await this.SendHeader(MessageType.Data, timestamp, dataSize, token).ConfigureAwait(false))
            {
                return false;
            }

            try
            {
                foreach (var buffer in buffers)
                {
                    await this.stream.WriteAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                NLogLogger.Error(ex, "Failed to write data to TCP recorder");
                return false;
            }

            return true;
        }

        public async Task<bool> SendLap(double timestamp, short number, byte triggerSource, string name, bool countForFastestLap, CancellationToken token)
        {
            byte[] buffer;
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(number);
                    binaryWriter.Write(triggerSource);
                    var nameBytes = Encoding.ASCII.GetBytes(name);
                    binaryWriter.Write(nameBytes.Length);
                    binaryWriter.Write(nameBytes);
                    binaryWriter.Write(countForFastestLap);
                }

                buffer = memoryStream.GetBuffer();
            }

            if (!await this.SendHeader(MessageType.Lap, timestamp, buffer.Length, token).ConfigureAwait(false))
            {
                return false;
            }

            try
            {
                await this.stream.WriteAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                NLogLogger.Error(ex, "Failed to write lap to TCP recorder");
                return false;
            }

            return true;
        }

        private async Task<bool> SendHeader(MessageType messageType, double timestamp, int dataLength, CancellationToken token)
        {
            var headerSize = Marshal.SizeOf<MessageHeader>();
            var messageSize = (ushort)(headerSize + dataLength);

            var messageHeader = new MessageHeader
            {
                Type = messageType,
                Version = MessageProtocolVersion,
                Size = messageSize,
                Timestamp = timestamp
            };

            var headerBytes = BufferUtilities.ToBytes(messageHeader);
            try
            {
                await this.stream.WriteAsync(headerBytes, 0, headerBytes.Length, token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                NLogLogger.Error(ex, "Failed to write header to TCP recorder");
                return false;
            }

            return true;
        }

        private enum MessageType : ushort
        {
            Config = 150,
            Data = 151,
            Lap = 152
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct MessageHeader
        {
            public MessageType Type;
            public ushort Version;
            public uint Size;
            public double Timestamp;
        }
    }
}