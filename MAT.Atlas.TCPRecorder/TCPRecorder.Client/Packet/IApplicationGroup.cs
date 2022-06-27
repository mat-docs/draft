// <copyright file="IApplicationGroup.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

namespace TCPRecorder.Client.Packet
{
    public interface IApplicationGroup
    {
        string Description { get; }

        string Name { get; }
    }
}