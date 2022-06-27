// <copyright file="ParameterDetails.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client.Parameters
{
    public sealed class ParameterDetails : FieldDetails, IParameterDetails
    {
        public ParameterDetails(
            string name,
            string description = null,
            (double, double)? minMax = null,
            string units = null,
            string format = null)
            : base(name, description)
        {
            this.MinMax = minMax;
            this.Units = units;
            this.Format = format;
        }

        public string Format { get; }

        public (double Min, double Max)? MinMax { get; }

        public string Units { get; }
    }
}