// <copyright file="PacketBuilderBase.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TCPRecorder.Client.Packet
{
    [DebuggerDisplay(
        nameof(PacketId) + " = {" + nameof(PacketId) + "}, " +
        nameof(ApplicationGroup) + " = {" + nameof(ApplicationGroup) + "}, " +
        nameof(ParameterGroup) + " = {" + nameof(ParameterGroup) + "}, " +
        nameof(ByteCount) + " = {" + nameof(ByteCount) + "}")]
    public abstract class PacketBuilderBase<TPacketData>
        where TPacketData : PacketDataBase
    {
        private readonly List<PacketField<TPacketData>> fields = new List<PacketField<TPacketData>>();

        protected PacketBuilderBase(IParameterGroup parameterGroup)
        {
            this.ParameterGroup = parameterGroup;
        }

        protected PacketBuilderBase(int? packetId, IParameterGroup parameterGroup)
        {
            this.PacketId = packetId;
            this.ParameterGroup = parameterGroup;
        }

        protected PacketBuilderBase(int? packetId, IApplicationGroup applicationGroup, IParameterGroup parameterGroup)
        {
            this.PacketId = packetId;
            this.ApplicationGroup = applicationGroup;
            this.ParameterGroup = parameterGroup;
        }

        public IApplicationGroup ApplicationGroup { get; }

        public int ByteCount { get; private set; }

        public IReadOnlyList<PacketField<TPacketData>> Fields => this.fields;

        public int? PacketId { get; }

        public IParameterGroup ParameterGroup { get; }

        public PacketBuilderBase<TPacketData> Array(int dimension)
        {
            if (dimension < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension), $"{nameof(dimension)} must be > 1");
            }

            return this.SetArray(field => field.SetArray(dimension));
        }

        public PacketBuilderBase<TPacketData> Array(IReadOnlyList<string> elementSuffixes)
        {
            if (elementSuffixes.Count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(elementSuffixes), $"{nameof(elementSuffixes)} must have at least 2 elements");
            }

            return this.SetArray(field => field.SetArray(elementSuffixes));
        }

        public PacketBuilderBase<TPacketData> Array(IReadOnlyList<IParameterDetails> standardParameters)
        {
            if (standardParameters.Count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(standardParameters), $"{nameof(standardParameters)} must have at least 2 elements");
            }

            return this.SetArray(field => field.SetArray(standardParameters));
        }

        public PacketBuilderBase<TPacketData> BitField(uint index, uint count)
        {
            var field = this.Fields.LastOrDefault();
            field?.SetBitField(index, count);
            return this;
        }

        public PacketBuilderBase<TPacketData> Calculated(Func<IPacketParameter<TPacketData>, TPacketData, object> getValue)
        {
            var field = this.Fields.LastOrDefault();
            field?.SetCalculated(getValue);
            return this;
        }

        public PacketBuilderBase<TPacketData> Converter(Func<IPacketParameter<TPacketData>, TPacketData, object, object> convertValue)
        {
            var field = this.Fields.LastOrDefault();
            field?.SetConverter(convertValue);
            return this;
        }

        public PacketBuilderBase<TPacketData> Details(IParameterDetails parameter, string name = null, string description = null)
        {
            var field = this.Fields.LastOrDefault();
            if (field == null)
            {
                return this;
            }

            field.SetDetails(name ?? parameter.Name, description ?? parameter.Description);

            if (parameter.MinMax.HasValue)
            {
                field.SetMinMax(parameter.MinMax.Value.Min, parameter.MinMax.Value.Max);
            }

            if (!string.IsNullOrWhiteSpace(parameter.Units))
            {
                field.SetUnits(parameter.Units);
            }

            if (!string.IsNullOrWhiteSpace(parameter.Format))
            {
                field.SetFormat(parameter.Format);
            }

            return this;
        }

        public PacketBuilderBase<TPacketData> Details(
            string name,
            string description = null,
            IApplicationGroup applicationGroup = null,
            IParameterGroup parameterGroup = null)
        {
            var field = this.Fields.LastOrDefault();
            field?.SetDetails(name, description, applicationGroup, parameterGroup);
            return this;
        }

        public PacketBuilderBase<TPacketData> Details(IParameterGroup parameterGroup)
        {
            var field = this.Fields.LastOrDefault();
            field?.SetDetails(parameterGroup);
            return this;
        }

        public PacketBuilderBase<TPacketData> Details(IApplicationGroup applicationGroup, IParameterGroup parameterGroup = null)
        {
            var field = this.Fields.LastOrDefault();
            field?.SetDetails(applicationGroup, parameterGroup);
            return this;
        }

        public PacketBuilderBase<TPacketData> Format(string format)
        {
            var field = this.Fields.LastOrDefault();
            field?.SetFormat(format);
            return this;
        }

        public PacketBuilderBase<TPacketData> GetField(out PacketField<TPacketData> field)
        {
            field = this.Fields.LastOrDefault();
            return this;
        }

        public PacketBuilderBase<TPacketData> MinMax(double minimum, double maximum)
        {
            var field = this.Fields.LastOrDefault();
            field?.SetMinMax(minimum, maximum);
            return this;
        }

        public PacketBuilderBase<TPacketData> MinMax((double minimum, double maximum) value) => this.MinMax(value.minimum, value.maximum);

        public PacketBuilderBase<TPacketData> Units(string units)
        {
            var field = this.Fields.LastOrDefault();
            field?.SetUnits(units);
            return this;
        }

        internal PacketBuilderBase<TPacketData> Add(PacketFieldType packetType, int size)
        {
            var field = new PacketField<TPacketData>(this.ByteCount, packetType, size);
            return this.AddField(field);
        }

        internal PacketBuilderBase<TPacketData> Add(PacketBuilderBase<TPacketData> structPacketBuilder)
        {
            var field = new PacketField<TPacketData>(this.ByteCount, structPacketBuilder);
            return this.AddField(field);
        }

        internal PacketBuilderBase<TPacketData> Add(int byteCount)
        {
            var field = new PacketField<TPacketData>(this.ByteCount, byteCount);
            return this.AddField(field);
        }

        internal PacketBuilderBase<TPacketData> AddField(PacketField<TPacketData> field)
        {
            this.fields.Add(field);

            this.ByteCount += field.ByteCount;
            return this;
        }

        internal void SetField(int i, PacketField<TPacketData> field)
        {
            this.fields[i] = field;
        }

        private PacketBuilderBase<TPacketData> SetArray(Action<PacketField<TPacketData>> setArrayAction)
        {
            var field = this.Fields.LastOrDefault();
            if (field == null)
            {
                return this;
            }

            var byteCountBefore = field.ByteCount;
            setArrayAction(field);
            var byteCountAfter = field.ByteCount;

            this.ByteCount += byteCountAfter - byteCountBefore;
            return this;
        }
    }
}