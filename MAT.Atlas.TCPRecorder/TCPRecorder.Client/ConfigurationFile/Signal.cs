// <copyright file="Signal.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System.Collections.Generic;
using System.Linq;

using TCPRecorder.Client.Extensions;

namespace TCPRecorder.Client.ConfigurationFile
{
    public sealed class Signal
    {
        public Signal(
            string signalName,
            DataType dataType,
            DataSource dataSource,
            double frequency,
            uint startByte,
            uint? numBits = null,
            uint startBit = default)
        {
            this.SignalName = signalName;
            this.DataType = dataType;
            this.DataSource = dataSource;
            this.Frequency = frequency;
            this.StartByte = startByte;
            this.NumBits = numBits ?? dataType.BitCount();
            this.StartBit = startBit;
        }

        public DataSource DataSource { get; set; }

        public DataType DataType { get; set; }

        public double Frequency { get; set; }

        public uint NumBits { get; set; }

        public string SignalName { get; set; }

        public uint StartBit { get; set; }

        public uint StartByte { get; set; }

        public static List<Signal> CreateSignals(params Signal[] signals) => signals.ToList();
    }
}