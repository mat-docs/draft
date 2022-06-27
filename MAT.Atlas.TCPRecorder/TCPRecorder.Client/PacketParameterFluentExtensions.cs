// <copyright file="PacketParameterFluentExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Collections.Generic;
using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client
{
    public static class PacketParameterFluentExtensions
    {
        public static IPacketParameter<TPacketData> ApplicationGroup<TPacketData>(this IPacketParameter<TPacketData> packetParameter, string applicationGroup)
            where TPacketData : PacketDataBase => packetParameter.SetApplicationGroup(applicationGroup);

        public static IPacketParameter<TPacketData> BitField<TPacketData>(this IPacketParameter<TPacketData> packetParameter, uint index, uint count)
            where TPacketData : PacketDataBase => packetParameter.SetBitField(index, count);

        public static IPacketParameter<TPacketData> Calculated<TPacketData>(
            this IPacketParameter<TPacketData> packetParameter,
            Func<IPacketParameter<TPacketData>, TPacketData, object> getValue)
            where TPacketData : PacketDataBase => packetParameter.SetCalculated(getValue);

        public static IPacketParameter<TPacketData> Converter<TPacketData>(
            this IPacketParameter<TPacketData> packetParameter,
            Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue)
            where TPacketData : PacketDataBase => packetParameter.SetConverter(convertValue);

        public static IPacketParameter<TPacketData> Format<TPacketData>(this IPacketParameter<TPacketData> packetParameter, string format)
            where TPacketData : PacketDataBase => packetParameter.SetFormat(format);

        public static IPacketParameter<TPacketData> Group<TPacketData>(this IPacketParameter<TPacketData> packetParameter, IReadOnlyCollection<string> groupPath)
            where TPacketData : PacketDataBase => packetParameter.SetGroupPath(groupPath);

        public static IPacketParameter<TPacketData> MinMax<TPacketData>(this IPacketParameter<TPacketData> packetParameter, (double minimum, double maximum) value)
            where TPacketData : PacketDataBase => packetParameter.SetMinMax(value.minimum, value.maximum);

        public static IPacketParameter<TPacketData> MinMax<TPacketData>(this IPacketParameter<TPacketData> packetParameter, double minimum, double maximum)
            where TPacketData : PacketDataBase => packetParameter.SetMinMax(minimum, maximum);

        public static IPacketParameter<TPacketData> SourceOffset<TPacketData>(this IPacketParameter<TPacketData> packetParameter, int packetId, int sourceOffset)
            where TPacketData : PacketDataBase
        {
            packetParameter.SetPacketId(packetId);
            return packetParameter.SetSourceByteOffset(sourceOffset);
        }

        public static IPacketParameter<TPacketData> TargetOffset<TPacketData>(this IPacketParameter<TPacketData> packetParameter, int targetOffset)
            where TPacketData : PacketDataBase => packetParameter.SetTargetByteOffset(targetOffset);

        public static IPacketParameter<TPacketData> Units<TPacketData>(this IPacketParameter<TPacketData> packetParameter, string units)
            where TPacketData : PacketDataBase => packetParameter.SetUnits(units);
    }
}