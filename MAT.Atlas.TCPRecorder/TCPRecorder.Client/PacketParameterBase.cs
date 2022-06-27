// <copyright file="PacketParameterBase.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using TCPRecorder.Client.Extensions;
using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client
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
            this.Name = name;
            this.Description = description;
            this.ApplicationGroup = applicationGroup;
            this.GroupPath = groupPath;
            this.PacketType = packetType;
            this.ByteCount = (int)this.PacketType.ByteCount();
            this.Minimum = displayRange?.minimum ?? this.PacketType.MinValue();
            this.Maximum = displayRange?.maximum ?? this.PacketType.MaxValue();
            this.Units = units ?? string.Empty;
            this.Format = format;
            this.StartBit = bitField?.index ?? 0;
            this.NumBits = bitField?.count ?? this.PacketType.BitCount();
            this.GetValue = getValue;
            this.ConvertValue = convertValue;
            this.PacketId = packetId;
            this.ArrayIndex = arrayIndex;
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
            this.ApplicationGroup = applicationGroup;
            return this;
        }

        public IPacketParameter<TPacketData> SetBitField(uint index, uint count)
        {
            this.StartBit = index;
            this.NumBits = count;
            return this;
        }

        public IPacketParameter<TPacketData> SetCalculated(Func<IPacketParameter<TPacketData>, TPacketData, object> getValue)
        {
            this.GetValue = getValue;
            return this;
        }

        public IPacketParameter<TPacketData> SetConverter(Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue)
        {
            this.ConvertValue = convertValue;
            return this;
        }

        public IPacketParameter<TPacketData> SetFormat(string format)
        {
            this.Format = format;
            return this;
        }

        public IPacketParameter<TPacketData> SetGroupPath(IReadOnlyCollection<string> groupPath)
        {
            this.GroupPath = groupPath;
            return this;
        }

        public IPacketParameter<TPacketData> SetMinMax(double minimum, double maximum)
        {
            this.Minimum = Math.Min(minimum, maximum);
            this.Maximum = Math.Max(minimum, maximum);
            return this;
        }

        public IPacketParameter<TPacketData> SetPacketId(int? id)
        {
            this.PacketId = id;
            return this;
        }

        public IPacketParameter<TPacketData> SetSourceByteOffset(int offset)
        {
            this.SourceByteOffset = offset;
            return this;
        }

        public IPacketParameter<TPacketData> SetTargetByteOffset(int offset)
        {
            this.TargetByteOffset = offset;
            return this;
        }

        public IPacketParameter<TPacketData> SetUnits(string units)
        {
            this.Units = units;
            return this;
        }
    }
}