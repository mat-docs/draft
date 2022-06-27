// <copyright file="ParameterGroup.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client
{
    public sealed class ParameterGroup : FieldDetails, IParameterGroup
    {
        public ParameterGroup(string name, string description = null)
            : base(name, description)
        {
        }
    }
}