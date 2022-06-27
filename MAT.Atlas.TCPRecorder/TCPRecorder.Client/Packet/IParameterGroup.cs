// <copyright file="IParameterGroup.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.Packet
{
    public interface IParameterGroup
    {
        string Description { get; }

        string Name { get; }
    }
}