// <copyright file="IParameterGroup.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

namespace TCPRecorder.Client.Packet
{
    public interface IParameterGroup
    {
        string Description { get; }

        string Name { get; }
    }
}