// <copyright file="PacketParameter.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Collections.Generic;
using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client
{
    public sealed class PacketParameter : PacketParameterBase<DefaultPacketData>
    {
        public PacketParameter(
            string name,
            string description,
            PacketFieldType packetType,
            (double minimum, double maximum)? displayRange = null,
            string units = null,
            string format = null,
            (uint index, uint count)? bitField = null,
            Func<IPacketParameter<DefaultPacketData>, DefaultPacketData, object> getValue = null,
            Func<IPacketParameter<DefaultPacketData>, DefaultPacketData, object, object> convertValue = null,
            string applicationGroup = null,
            IReadOnlyCollection<string> groupPath = null,
            int? packetId = null,
            int arrayIndex = 0)
            : base(name, description, applicationGroup, groupPath, packetType, displayRange, units, format, bitField, getValue, convertValue, packetId, arrayIndex)
        {
        }
    }
}