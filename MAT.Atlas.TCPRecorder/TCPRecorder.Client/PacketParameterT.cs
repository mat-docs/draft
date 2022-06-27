// <copyright file="PacketParameterT.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Collections.Generic;
using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client
{
    public sealed class PacketParameterT<TPacketData> : PacketParameterBase<TPacketData>
        where TPacketData : PacketDataBase
    {
        public PacketParameterT(
            string name,
            string description,
            PacketFieldType packetType,
            (double minimum, double maximum)? displayRange = null,
            string units = null,
            string format = null,
            (uint index, uint count)? bitField = null,
            Func<IPacketParameter<TPacketData>, TPacketData, object> getValue = null,
            Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue = null,
            string applicationGroup = null,
            IReadOnlyCollection<string> groupPath = null,
            int? packetId = null,
            int arrayIndex = 0)
            : base(name, description, applicationGroup, groupPath, packetType, displayRange, units, format, bitField, getValue, convertValue, packetId, arrayIndex)
        {
        }

        public PacketParameterT(
            PacketField<TPacketData> field,
            string name = null,
            string description = null,
            PacketFieldType? packetType = null,
            (double minimum, double maximum)? displayRange = null,
            string units = null,
            string format = null,
            (uint index, uint count)? bitField = null,
            Func<IPacketParameter<TPacketData>, TPacketData, object> getValue = null,
            Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue = null,
            string applicationGroup = null,
            IReadOnlyCollection<string> groupPath = null,
            int? packetId = null,
            int arrayIndex = 0)
            : base(field, name, description, applicationGroup, groupPath, packetType, displayRange, units, format, bitField, getValue, convertValue, packetId, arrayIndex)
        {
        }
    }
}