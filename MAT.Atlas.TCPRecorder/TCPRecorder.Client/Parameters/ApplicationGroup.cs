// <copyright file="ApplicationGroup.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client.Parameters
{
    public sealed class ApplicationGroup : FieldDetails, IApplicationGroup
    {
        public ApplicationGroup(string name, string description = null)
            : base(name, description)
        {
        }
    }
}