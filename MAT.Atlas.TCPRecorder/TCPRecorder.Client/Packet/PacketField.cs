// <copyright file="PacketField.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using TCPRecorder.Client.Extensions;

namespace TCPRecorder.Client.Packet
{
    [DebuggerDisplay(
        nameof(Name) + " = {" + nameof(Name) + "}, " +
        nameof(Dimension) + " = {" + nameof(Dimension) + "}, " +
        nameof(ByteOffset) + " = {" + nameof(ByteOffset) + "}, " +
        nameof(ByteCount) + " = {" + nameof(ByteCount) + "}")]
    public sealed class PacketField<TPacketData>
        where TPacketData : PacketDataBase
    {
        private readonly PacketFieldType? type;

        public PacketField(int byteOffset, int elementByteCount)
        {
            if (byteOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(byteOffset), $"{nameof(byteOffset)} must be >= 0");
            }

            this.ByteOffset = byteOffset;
            this.ElementByteCount = elementByteCount;
            this.CalculateByteCount();
        }

        public PacketField(int byteOffset, PacketFieldType type, int size)
            : this(byteOffset, (int)type.ByteCount((uint)size))
        {
            this.type = type;
        }

        public PacketField(int byteOffset, PacketBuilderBase<TPacketData> structPacketBuilder)
            : this(byteOffset, structPacketBuilder.ByteCount)
        {
            this.StructPacketBuilder = structPacketBuilder;
        }

        internal PacketField(
            PacketField<TPacketData> copy,
            string name = null,
            string description = null,
            IApplicationGroup applicationGroup = null,
            IParameterGroup parameterGroup = null,
            PacketFieldType? type = null,
            int? byteOffset = null,
            int? elementByteCount = null,
            IReadOnlyList<string> elementSuffixes = null,
            IReadOnlyList<IParameterDetails> standardParameters = null,
            int? dimension = null,
            PacketBuilderBase<TPacketData> structPacketBuilder = null,
            (uint index, uint count)? bitField = null,
            (double maximum, double minimum)? displayRange = null,
            string units = null,
            string format = null,
            Func<IPacketParameter<TPacketData>, TPacketData, object> getValue = null,
            Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue = null)
        {
            this.Name = name ?? copy.Name;
            this.Description = description ?? copy.Description;
            this.ApplicationGroup = applicationGroup ?? copy.ApplicationGroup;
            this.ParameterGroup = parameterGroup ?? copy.ParameterGroup;
            this.type = type ?? copy.type;
            this.ByteOffset = byteOffset ?? copy.ByteOffset;
            this.ElementByteCount = elementByteCount ?? copy.ElementByteCount;
            this.ElementSuffixes = elementSuffixes ?? copy.ElementSuffixes;
            this.StandardParameters = standardParameters ?? copy.StandardParameters;
            this.Dimension = dimension ?? copy.Dimension;
            this.StructPacketBuilder = structPacketBuilder ?? copy.StructPacketBuilder;
            this.BitField = bitField ?? copy.BitField;
            this.DisplayRange = displayRange ?? copy.DisplayRange;
            this.Units = units ?? copy.Units;
            this.Format = format ?? copy.Format;
            this.GetValue = getValue ?? copy.GetValue;
            this.ConvertValue = convertValue ?? copy.ConvertValue;
            this.CalculateByteCount();
        }

        public IApplicationGroup ApplicationGroup { get; private set; }

        public (uint Index, uint Count)? BitField { get; private set; }

        public int ByteCount { get; private set; }

        public int ByteOffset { get; }

        public Func<IPacketParameter<TPacketData>, TPacketData, object, object> ConvertValue { get; private set; }

        public string Description { get; private set; }

        public int Dimension { get; private set; } = 1;

        public (double Maximum, double Minimum)? DisplayRange { get; private set; }

        public int ElementByteCount { get; }

        public IReadOnlyList<string> ElementSuffixes { get; private set; }

        public string Format { get; private set; }

        public Func<IPacketParameter<TPacketData>, TPacketData, object> GetValue { get; private set; }

        public bool IsArray => this.Dimension > 1;

        public bool IsCalculated => this.GetValue != null;

        public bool IsConverted => this.ConvertValue != null;

        public bool IsPadding => !this.type.HasValue && this.StructPacketBuilder == null;

        public bool IsSkipped => (string.IsNullOrWhiteSpace(this.Name) && this.ApplicationGroup == null && this.ParameterGroup == null) || this.IsPadding;

        public bool IsString => this.Type == PacketFieldType.String;

        public bool IsStruct => this.StructPacketBuilder != null;

        public string Name { get; private set; }

        public IParameterGroup ParameterGroup { get; private set; }

        public IReadOnlyList<IParameterDetails> StandardParameters { get; private set; }

        public PacketBuilderBase<TPacketData> StructPacketBuilder { get; private set; }

        public PacketFieldType Type
        {
            get
            {
                if (!this.type.HasValue)
                {
                    throw new InvalidOperationException("Type not known for composite fields");
                }

                return this.type.Value;
            }
        }

        public string Units { get; private set; }

        public int GetByteOffsetAtIndex(int index)
        {
            if (!this.IsArray)
            {
                throw new ArgumentException("Field must be an array");
            }

            if (index < 0 ||
                index >= this.Dimension)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} must be >= 0 and < {this.Dimension}");
            }

            return this.ByteOffset + index * this.ElementByteCount;
        }

        public object GetElementSuffixAtIndex(int index)
        {
            if (!this.IsArray)
            {
                throw new ArgumentException("Field must be an array");
            }

            if (index < 0 ||
                index >= this.Dimension)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} must be >= 0 and < {this.Dimension}");
            }

            if (this.ElementSuffixes != null)
            {
                return this.ElementSuffixes[index];
            }

            return index + 1;
        }

        public (string Name, string Description) GetParameterDetails(int index)
        {
            if (!this.IsArray)
            {
                return (this.Name, this.Description);
            }

            if (index < 0 ||
                index >= this.Dimension)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} must be >= 0 and < {this.Dimension}");
            }

            if (this.StandardParameters?.Count > 0)
            {
                return (this.StandardParameters[index].Name, this.StandardParameters[index].Description);
            }

            return (string.Concat(this.Name, this.GetElementSuffixAtIndex(index)), this.Description);
        }

        internal PacketField<TPacketData> SetArray(int dimension)
        {
            this.Dimension = dimension;
            this.CalculateByteCount();
            return this;
        }

        internal PacketField<TPacketData> SetArray(IReadOnlyList<string> elementSuffixes)
        {
            this.ElementSuffixes = elementSuffixes;
            this.Dimension = elementSuffixes.Count;
            this.CalculateByteCount();
            return this;
        }

        internal PacketField<TPacketData> SetArray(IReadOnlyList<IParameterDetails> standardParameters)
        {
            this.StandardParameters = standardParameters;
            this.Dimension = standardParameters.Count;
            this.CalculateByteCount();
            return this;
        }

        internal void SetBitField(uint index, uint count)
        {
            this.BitField = (index, count);
        }

        internal void SetCalculated(Func<IPacketParameter<TPacketData>, TPacketData, object> getValue)
        {
            if (this.IsStruct)
            {
                var packetBuilder = new PacketBuilderT<TPacketData>();
                foreach (var field in this.StructPacketBuilder.Fields)
                {
                    if (field.GetValue != null)
                    {
                        throw new InvalidOperationException("Cannot override calculated field");
                    }

                    var newField = new PacketField<TPacketData>(field);
                    newField.SetCalculated(getValue);
                    packetBuilder.AddField(newField);
                }

                this.StructPacketBuilder = packetBuilder;
            }
            else
            {
                this.GetValue = getValue;
            }
        }

        internal void SetConverter(Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue)
        {
            this.ConvertValue = convertValue;
        }

        internal void SetDetails(string name, string description, IApplicationGroup applicationGroup = null, IParameterGroup parameterGroup = null)
        {
            this.Name = name;
            this.Description = description;
            this.ApplicationGroup = applicationGroup;
            this.ParameterGroup = parameterGroup;
        }

        internal void SetDetails(IParameterGroup parameterGroup)
        {
            this.ParameterGroup = parameterGroup;
            this.Name = parameterGroup.Name;
            this.Description = parameterGroup.Description;
        }

        internal void SetDetails(IApplicationGroup applicationGroup, IParameterGroup parameterGroup = null)
        {
            this.ApplicationGroup = applicationGroup;
            this.ParameterGroup = parameterGroup;
            this.Name = applicationGroup.Name;
            this.Description = applicationGroup.Description;
        }

        internal void SetFormat(string format)
        {
            this.Format = format;
        }

        internal void SetMinMax(double minimum, double maximum)
        {
            this.DisplayRange = (minimum, maximum);
        }

        internal void SetUnits(string units)
        {
            this.Units = units;
        }

        private void CalculateByteCount()
        {
            if (this.ElementByteCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.ElementByteCount), $"{nameof(this.ElementByteCount)} must be > 0");
            }

            if (this.Dimension <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.Dimension), $"{nameof(this.Dimension)} must be > 0");
            }

            this.ByteCount = this.ElementByteCount * this.Dimension;
        }
    }
}