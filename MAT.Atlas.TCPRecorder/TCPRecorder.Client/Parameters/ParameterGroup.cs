// <copyright file="ParameterGroup.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client.Parameters
{
    public sealed class ParameterGroup : FieldDetails, IParameterGroup
    {
        public ParameterGroup(string name, string description = null)
            : base(name, description)
        {
        }
    }
}