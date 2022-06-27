// <copyright file="PacketParameterBase.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;

using TCPRecorder.Client.Extensions;
using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client.Parameters
{
    [DebuggerDisplay(
        nameof(Name) + " = {" + nameof(Name) + "}, " +
        nameof(ApplicationGroup) + " = {" + nameof(ApplicationGroup) + "}, " +
        nameof(PacketType) + " = {" + nameof(PacketType) + "}, " +
        nameof(SourceByteOffset) + " = {" + nameof(SourceByteOffset) + "}, " +
        nameof(TargetByteOffset) + " = {" + nameof(TargetByteOffset) + "}")]
    public abstract class PacketParameterBase<TPacketData> : IPacketParameter<TPacketData>
        where TPacketData : PacketDataBase
    {
        protected PacketParameterBase(
            string name,
            string description,
            string applicationGroup,
            IReadOnlyCollection<string> groupPath,
            PacketFieldType packetType,
            (double minimum, double maximum)? displayRange,
            string units,
            string format,
            (uint index, uint count)? bitField,
            Func<IPacketParameter<TPacketData>, TPacketData, object> getValue,
            Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue,
            int? packetId,
            int arrayIndex)
        {
            Name = name;
            Description = description;
            ApplicationGroup = applicationGroup;
            GroupPath = groupPath;
            PacketType = packetType;
            ByteCount = (int)PacketType.ByteCount();
            Minimum = displayRange?.minimum ?? PacketType.MinValue();
            Maximum = displayRange?.maximum ?? PacketType.MaxValue();
            Units = units ?? string.Empty;
            Format = format;
            StartBit = bitField?.index ?? 0;
            NumBits = bitField?.count ?? PacketType.BitCount();
            GetValue = getValue;
            ConvertValue = convertValue;
            PacketId = packetId;
            ArrayIndex = arrayIndex;
        }

        protected PacketParameterBase(
            PacketField<TPacketData> field,
            string name,
            string description,
            string applicationGroup,
            IReadOnlyCollection<string> groupPath,
            PacketFieldType? packetType,
            (double minimum, double maximum)? displayRange,
            string units,
            string format,
            (uint index, uint count)? bitField,
            Func<IPacketParameter<TPacketData>, TPacketData, object> getValue,
            Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue,
            int? packetId,
            int arrayIndex)
            : this(
                name ?? field.Name,
                description ?? field.Description,
                applicationGroup,
                groupPath,
                packetType ?? field.Type,
                displayRange ?? field.DisplayRange,
                units ?? field.Units,
                format ?? field.Format,
                bitField ?? field.BitField,
                getValue ?? field.GetValue,
                convertValue ?? field.ConvertValue,
                packetId,
                arrayIndex)
        {
        }

        public string ApplicationGroup { get; private set; }

        public int ArrayIndex { get; }

        public int ByteCount { get; }

        public Func<IPacketParameter<TPacketData>, TPacketData, object, object> ConvertValue { get; private set; }

        public string Description { get; }

        public string Format { get; private set; }

        public Func<IPacketParameter<TPacketData>, TPacketData, object> GetValue { get; private set; }

        public IReadOnlyCollection<string> GroupPath { get; private set; }

        public double Maximum { get; private set; }

        public double Minimum { get; private set; }

        public string Name { get; }

        public uint NumBits { get; private set; }

        public int? PacketId { get; private set; }

        public PacketFieldType PacketType { get; }

        public int SourceByteOffset { get; private set; } = -1;

        public uint StartBit { get; private set; }

        public int TargetByteOffset { get; private set; } = -1;

        public string Units { get; private set; }

        public IPacketParameter<TPacketData> SetApplicationGroup(string applicationGroup)
        {
            ApplicationGroup = applicationGroup;
            return this;
        }

        public IPacketParameter<TPacketData> SetBitField(uint index, uint count)
        {
            StartBit = index;
            NumBits = count;
            return this;
        }

        public IPacketParameter<TPacketData> SetCalculated(Func<IPacketParameter<TPacketData>, TPacketData, object> getValue)
        {
            GetValue = getValue;
            return this;
        }

        public IPacketParameter<TPacketData> SetConverter(Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue)
        {
            ConvertValue = convertValue;
            return this;
        }

        public IPacketParameter<TPacketData> SetFormat(string format)
        {
            Format = format;
            return this;
        }

        public IPacketParameter<TPacketData> SetGroupPath(IReadOnlyCollection<string> groupPath)
        {
            GroupPath = groupPath;
            return this;
        }

        public IPacketParameter<TPacketData> SetMinMax(double minimum, double maximum)
        {
            Minimum = Math.Min(minimum, maximum);
            Maximum = Math.Max(minimum, maximum);
            return this;
        }

        public IPacketParameter<TPacketData> SetPacketId(int? id)
        {
            PacketId = id;
            return this;
        }

        public IPacketParameter<TPacketData> SetSourceByteOffset(int offset)
        {
            SourceByteOffset = offset;
            return this;
        }

        public IPacketParameter<TPacketData> SetTargetByteOffset(int offset)
        {
            TargetByteOffset = offset;
            return this;
        }

        public IPacketParameter<TPacketData> SetUnits(string units)
        {
            Units = units;
            return this;
        }
    }
}