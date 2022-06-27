// <copyright file="IPacketParameter.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Collections.Generic;

namespace TCPRecorder.Client.Packet
{
    public interface IPacketParameter<TPacketData>
        where TPacketData : PacketDataBase
    {
        string ApplicationGroup { get; }

        int ArrayIndex { get; }

        int ByteCount { get; }

        Func<IPacketParameter<TPacketData>, TPacketData, object, object> ConvertValue { get; }

        string Description { get; }

        string Format { get; }

        Func<IPacketParameter<TPacketData>, TPacketData, object> GetValue { get; }

        IReadOnlyCollection<string> GroupPath { get; }

        double Maximum { get; }

        double Minimum { get; }

        string Name { get; }

        uint NumBits { get; }

        int? PacketId { get; }

        PacketFieldType PacketType { get; }

        int SourceByteOffset { get; }

        uint StartBit { get; }

        int TargetByteOffset { get; }

        string Units { get; }

        IPacketParameter<TPacketData> SetApplicationGroup(string applicationGroup);

        IPacketParameter<TPacketData> SetBitField(uint index, uint count);

        IPacketParameter<TPacketData> SetCalculated(Func<IPacketParameter<TPacketData>, TPacketData, object> getValue);

        IPacketParameter<TPacketData> SetConverter(Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue);

        IPacketParameter<TPacketData> SetFormat(string format);

        IPacketParameter<TPacketData> SetGroupPath(IReadOnlyCollection<string> groupPath);

        IPacketParameter<TPacketData> SetMinMax(double minimum, double maximum);

        IPacketParameter<TPacketData> SetPacketId(int? id);

        IPacketParameter<TPacketData> SetSourceByteOffset(int offset);

        IPacketParameter<TPacketData> SetTargetByteOffset(int offset);

        IPacketParameter<TPacketData> SetUnits(string units);
    }
}