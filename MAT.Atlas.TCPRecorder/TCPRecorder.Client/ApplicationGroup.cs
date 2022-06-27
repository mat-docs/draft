// <copyright file="ApplicationGroup.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client
{
    public sealed class ApplicationGroup : FieldDetails, IApplicationGroup
    {
        public ApplicationGroup(string name, string description = null)
            : base(name, description)
        {
        }
    }
}