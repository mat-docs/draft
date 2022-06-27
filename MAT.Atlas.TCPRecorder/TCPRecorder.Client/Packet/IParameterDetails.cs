// <copyright file="IParameterDetails.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

namespace TCPRecorder.Client.Packet
{
    public interface IParameterDetails
    {
        string Description { get; }

        string Format { get; }

        (double Min, double Max)? MinMax { get; }

        string Name { get; }

        string Units { get; }
    }
}