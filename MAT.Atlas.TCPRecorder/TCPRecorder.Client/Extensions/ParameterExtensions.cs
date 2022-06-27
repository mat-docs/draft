// <copyright file="ParameterExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using TCPRecorder.Client.ConfigurationFile;
using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client.Extensions
{
    public static class ParameterExtensions
    {
        private static readonly Logger NLogLogger = LogManager.GetCurrentClassLogger();

        public static uint BitCount(this DataType dataType) => ByteCount(dataType) * 8u;

        public static uint ByteCount(this DataType dataType)
        {
            switch (dataType)
            {
            case DataType.Char:
                return sizeof(sbyte);
            case DataType.Packed:
                return sizeof(byte);
            case DataType.Short:
                return sizeof(short);
            case DataType.UShort:
                return sizeof(ushort);
            case DataType.Int:
            case DataType.LLong:
                return sizeof(int);
            case DataType.UInt:
                return sizeof(uint);
            case DataType.Float:
            case DataType.TripleFloat:
                return sizeof(float);
            case DataType.Double:
                return sizeof(double);
            default:
                throw new InvalidOperationException("Unknown Type");
            }
        }

        public static string DefaultFormat(this DataType dataType)
        {
            switch (dataType)
            {
            case DataType.Char:
                return "%d";
            case DataType.Packed:
                return "%d";
            case DataType.Short:
                return "%d";
            case DataType.UShort:
                return "%d";
            case DataType.Int:
            case DataType.LLong:
                return "%d";
            case DataType.UInt:
                return "%d";
            case DataType.Float:
            case DataType.TripleFloat:
                return "%.5f";
            case DataType.Double:
                return "%.8f";
            default:
                throw new InvalidOperationException("Unknown Type");
            }
        }

        public static T GetSourceValue<TPacketData, T>(this IPacketParameter<TPacketData> parameter, byte[] source)
            where TPacketData : PacketDataBase
        {
            var obj = GetSourceValue(parameter, source);
            if (obj is T value)
            {
                return value;
            }

            throw new ArgumentException(nameof(parameter), $"{typeof(T).Name} is not a valid type for {parameter.Name}:{parameter.PacketType.ToDataType()}");
        }

        public static object GetSourceValue<TPacketData>(this IPacketParameter<TPacketData> parameter, byte[] source)
            where TPacketData : PacketDataBase
        {
            if (parameter.SourceByteOffset < 0 ||
                parameter.SourceByteOffset >= source.Length ||
                parameter.SourceByteOffset + parameter.ByteCount > source.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(parameter),
                    $"{parameter.Name}:{parameter.SourceByteOffset}+{parameter.ByteCount} is outside of {source.Length}");
            }

            return GetValue(parameter, source, parameter.SourceByteOffset);
        }

        public static T GetTargetValue<TPacketData, T>(this IPacketParameter<TPacketData> parameter, byte[] source)
            where TPacketData : PacketDataBase
        {
            var obj = GetTargetValue(parameter, source);
            if (obj is T value)
            {
                return value;
            }

            throw new ArgumentException(nameof(parameter), $"{typeof(T).Name} is not a valid type for {parameter.Name}:{parameter.PacketType.ToDataType()}");
        }

        public static object GetTargetValue<TPacketData>(this IPacketParameter<TPacketData> parameter, byte[] target)
            where TPacketData : PacketDataBase
        {
            if (parameter.TargetByteOffset < 0 ||
                parameter.TargetByteOffset >= target.Length ||
                parameter.TargetByteOffset + parameter.ByteCount > target.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(parameter),
                    $"{parameter.Name}:{parameter.TargetByteOffset}+{parameter.ByteCount} is outside of {target.Length}");
            }

            return GetValue(parameter, target, parameter.TargetByteOffset);
        }

        public static void MergePacket<TPacketData>(this IReadOnlyCollection<IPacketParameter<TPacketData>> parameters, TPacketData packetData)
            where TPacketData : PacketDataBase
        {
            foreach (var parameter in parameters)
            {
                if (parameter.GetValue != null ||
                    parameter.PacketId != packetData.PacketId)
                {
                    continue;
                }

                if (parameter.ConvertValue != null)
                {
                    var value = parameter.GetSourceValue(packetData.SourcePacket);
                    if (value != null)
                    {
                        value = parameter.ConvertValue(parameter, packetData, value);
                        if (value != null)
                        {
                            parameter.SetTargetValue(packetData.TargetPacket, value);
                        }
                    }
                }
                else
                {
                    var source = packetData.SourcePacket.AsSpan(parameter.SourceByteOffset, parameter.ByteCount);
                    var target = packetData.TargetPacket.AsSpan(parameter.TargetByteOffset);
                    source.CopyTo(target);
                }
            }

            foreach (var parameter in parameters)
            {
                var value = parameter.GetValue?.Invoke(parameter, packetData);
                if (value != null &&
                    parameter.ConvertValue != null)
                {
                    value = parameter.ConvertValue(parameter, packetData, value);
                }

                if (value != null)
                {
                    parameter.SetTargetValue(packetData.TargetPacket, value);
                }
            }
        }

        public static void SetSourceValue<TPacketData>(this IPacketParameter<TPacketData> parameter, byte[] source, object value)
            where TPacketData : PacketDataBase
        {
            if (parameter.SourceByteOffset < 0 ||
                parameter.SourceByteOffset >= source.Length ||
                parameter.SourceByteOffset + parameter.ByteCount > source.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(parameter),
                    $"{parameter.Name}:{parameter.SourceByteOffset}+{parameter.ByteCount} is outside of {source.Length}");
            }

            SetValue(parameter, source, parameter.SourceByteOffset, value);
        }

        public static void SetTargetValue<TPacketData>(this IPacketParameter<TPacketData> parameter, byte[] target, object value)
            where TPacketData : PacketDataBase
        {
            if (parameter.TargetByteOffset < 0 ||
                parameter.TargetByteOffset >= target.Length ||
                parameter.TargetByteOffset + parameter.ByteCount > target.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(parameter),
                    $"{parameter.Name}:{parameter.TargetByteOffset}+{parameter.ByteCount} is outside of {target.Length}");
            }

            SetValue(parameter, target, parameter.TargetByteOffset, value);
        }

        public static DataType ToDataType(this PacketFieldType packetFieldType)
        {
            switch (packetFieldType)
            {
            case PacketFieldType.Char:
            case PacketFieldType.SignedChar:
            case PacketFieldType.Int8:
            case PacketFieldType.SByte:
                return DataType.Char;
            case PacketFieldType.Packed:
            case PacketFieldType.UnsignedChar:
            case PacketFieldType.UInt8:
            case PacketFieldType.Byte:
                return DataType.Packed;
            case PacketFieldType.Short:
            case PacketFieldType.SignedShort:
            case PacketFieldType.Int16:
                return DataType.Short;
            case PacketFieldType.UShort:
            case PacketFieldType.UnsignedShort:
            case PacketFieldType.UInt16:
            case PacketFieldType.Word:
                return DataType.UShort;
            case PacketFieldType.Int:
            case PacketFieldType.SignedInt:
            case PacketFieldType.Int32:
                return DataType.Int;
            case PacketFieldType.LLong:
                return DataType.LLong;
            case PacketFieldType.UInt:
            case PacketFieldType.UnsignedInt:
            case PacketFieldType.UInt32:
            case PacketFieldType.DWord:
                return DataType.UInt;
            case PacketFieldType.Float:
            case PacketFieldType.Single:
                return DataType.Float;
            case PacketFieldType.TripleFloat:
                return DataType.TripleFloat;
            case PacketFieldType.Double:
                return DataType.Double;
            default:
                throw new InvalidOperationException("Unknown Type");
            }
        }

        public static string ToHex(this byte[] buffer)
        {
            if (buffer == null ||
                buffer.Length == 0)
            {
                return string.Empty;
            }

            var text = new StringBuilder(buffer.Length * 2);
            foreach (var b in buffer)
            {
                text.Append(b.ToString("X2"));
            }

            return text.ToString();
        }

        public static ICollection<IPacketParameter<TPacketData>> ToParameters<TPacketData>(
            this PacketBuilderBase<TPacketData> packetBuilder,
            ICollection<IPacketParameter<TPacketData>> parameters,
            Predicate<IPacketParameter<TPacketData>> parameterFilter = null)
            where TPacketData : PacketDataBase
        {
            if (!packetBuilder.PacketId.HasValue)
            {
                throw new ArgumentException($"Top level packet expected: {nameof(packetBuilder.PacketId)} not specified", nameof(packetBuilder));
            }

            var applicationGroup = packetBuilder.ApplicationGroup;

            var groupPath = new List<IParameterGroup>();
            if (packetBuilder.ParameterGroup != null)
            {
                groupPath.Add(packetBuilder.ParameterGroup);
            }

            AddPacketParameters(parameters, parameterFilter, packetBuilder.Fields, packetBuilder.PacketId.Value, applicationGroup, groupPath);
            return parameters;
        }

        public static Type ToType(this DataType dataType)
        {
            switch (dataType)
            {
            case DataType.Char:
                return typeof(sbyte);
            case DataType.Packed:
                return typeof(byte);
            case DataType.Short:
                return typeof(short);
            case DataType.UShort:
                return typeof(ushort);
            case DataType.Int:
            case DataType.LLong:
                return typeof(int);
            case DataType.UInt:
                return typeof(uint);
            case DataType.Float:
            case DataType.TripleFloat:
                return typeof(float);
            case DataType.Double:
                return typeof(double);
            default:
                throw new InvalidOperationException("Unknown Type");
            }
        }

        public static void TraceSource<TPacketData>(this IEnumerable<IPacketParameter<TPacketData>> parameters, int? packetId, byte[] source)
            where TPacketData : PacketDataBase
        {
            NLogLogger.Trace($"Source Packet, PacketId: {packetId ?? -1} ByteCount: {source.Length} ");
            foreach (var parameter in parameters)
            {
                if (parameter.PacketId != packetId)
                {
                    continue;
                }

                NLogLogger.Trace($"{parameter.Name} = {parameter.GetSourceValue(source)}");
            }
        }

        public static void TraceTarget<TPacketData>(this IEnumerable<IPacketParameter<TPacketData>> parameters, byte[] target)
            where TPacketData : PacketDataBase
        {
            NLogLogger.Trace($"Target Packet, ByteCount: {target.Length}");
            foreach (var parameter in parameters)
            {
                NLogLogger.Trace($"{parameter.Name} = {parameter.GetTargetValue(target)}");
            }
        }

        private static void AddPacketField<TPacketData>(
            ICollection<IPacketParameter<TPacketData>> parameters,
            Predicate<IPacketParameter<TPacketData>> parameterFilter,
            int packetId,
            PacketField<TPacketData> field,
            IApplicationGroup applicationGroup,
            IReadOnlyCollection<IParameterGroup> groupPath,
            int byteOffset,
            IReadOnlyCollection<object> suffixes,
            int? arrayIndex)
            where TPacketData : PacketDataBase
        {
            if (field.IsStruct)
            {
                AddPacketParameters(
                    parameters,
                    parameterFilter,
                    field.StructPacketBuilder.Fields,
                    packetId,
                    applicationGroup,
                    groupPath,
                    byteOffset,
                    suffixes,
                    arrayIndex);
            }
            else if (!field.IsString)
            {
                var parameterIndex = arrayIndex ?? 0;
                var (parameterName, description) = field.GetParameterDetails(parameterIndex);
                if (suffixes.Count > 0)
                {
                    var parameterNameParts = new List<object>(suffixes.Count + 1);
                    parameterNameParts.Add(parameterName);
                    parameterNameParts.AddRange(suffixes);
                    parameterName = string.Join("_", parameterNameParts);
                }

                var parameter = new PacketParameterT<TPacketData>(
                        field,
                        parameterName,
                        description,
                        applicationGroup: applicationGroup?.Name,
                        groupPath: groupPath.Select(pg => pg.Name).ToArray(),
                        arrayIndex: parameterIndex)
                    .SourceOffset(packetId, byteOffset);

                if (parameterFilter?.Invoke(parameter) ?? true)
                {
                    parameters.Add(parameter);
                }
            }
        }

        private static void AddPacketParameters<TPacketData>(
            ICollection<IPacketParameter<TPacketData>> parameters,
            Predicate<IPacketParameter<TPacketData>> parameterFilter,
            IEnumerable<PacketField<TPacketData>> fields,
            int packetId,
            IApplicationGroup applicationGroup,
            IReadOnlyCollection<IParameterGroup> groupPath,
            int byteOffset = 0,
            IReadOnlyCollection<object> suffixes = null,
            int? arrayIndex = null)
            where TPacketData : PacketDataBase
        {
            foreach (var field in fields.Where(field => !field.IsSkipped))
            {
                var fieldApplicationGroup = applicationGroup;
                var fieldGroupPath = new List<IParameterGroup>(groupPath);
                var fieldSuffixes = new List<object>(suffixes ?? Array.Empty<object>());

                if (field.ApplicationGroup != null)
                {
                    if (fieldApplicationGroup != null)
                    {
                        throw new InvalidOperationException("There can only be one application group.");
                    }

                    if (fieldGroupPath.Any())
                    {
                        throw new InvalidOperationException("Application group must be head of group path.");
                    }
                }

                if (field.ParameterGroup != null)
                {
                    fieldGroupPath.Add(field.ParameterGroup);
                }
                else if (field.IsStruct)
                {
                    fieldGroupPath.Add(new ParameterGroup(field.Name, field.Description));
                }

                if (field.IsArray)
                {
                    for (var i = 0; i < field.Dimension; ++i)
                    {
                        var fieldArrayGroupPath = new List<IParameterGroup>(fieldGroupPath);
                        var fieldArraySuffixes = new List<object>(fieldSuffixes);
                        if (field.ApplicationGroup != null)
                        {
                            fieldApplicationGroup = new ApplicationGroup($"{field.ApplicationGroup.Name}{i + 1}", field.ApplicationGroup.Description);
                        }
                        else if (field.ParameterGroup != null ||
                                 field.IsStruct)
                        {
                            var lastParameterGroup = fieldArrayGroupPath[fieldArrayGroupPath.Count - 1];
                            var newParameterGroup = new ParameterGroup($"{lastParameterGroup.Name}{i + 1}", lastParameterGroup.Description);
                            fieldArrayGroupPath[fieldArrayGroupPath.Count - 1] = newParameterGroup;
                            fieldArraySuffixes.Add(field.GetElementSuffixAtIndex(i));
                        }

                        AddPacketField(
                            parameters,
                            parameterFilter,
                            packetId,
                            field,
                            fieldApplicationGroup,
                            fieldArrayGroupPath,
                            byteOffset + field.GetByteOffsetAtIndex(i),
                            fieldArraySuffixes,
                            i);
                    }
                }
                else
                {
                    AddPacketField(
                        parameters,
                        parameterFilter,
                        packetId,
                        field,
                        fieldApplicationGroup,
                        fieldGroupPath,
                        byteOffset + field.ByteOffset,
                        fieldSuffixes,
                        arrayIndex);
                }
            }
        }

        private static object GetValue<TPacketData>(IPacketParameter<TPacketData> parameter, byte[] buffer, int offset)
            where TPacketData : PacketDataBase
        {
            try
            {
                var value = GetValue(parameter.PacketType.ToDataType(), buffer, offset, parameter.ByteCount);

                if (parameter.StartBit > 0)
                {
                    var intValue = Convert.ToInt32(value);
                    var mask = (1 << (int)parameter.NumBits) - 1;
                    var shiftedToStart = intValue >> ((int)parameter.PacketType.BitCount() - (int)parameter.StartBit);
                    value = shiftedToStart & mask;
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(parameter), $"Error getting value of {parameter.Name}: {ex.Message}");
            }
        }

        private static object GetValue(DataType type, byte[] buffer, int offset, int count)
        {
            var span = buffer.AsSpan(offset, count);
            switch (type)
            {
            case DataType.Char:
                return (sbyte)span[0];
            case DataType.Packed:
                return span[0];
            case DataType.Short:
                return BitConverter.ToInt16(span);
            case DataType.UShort:
                return BitConverter.ToUInt16(span);
            case DataType.Int:
            case DataType.LLong:
                return BitConverter.ToInt32(span);
            case DataType.UInt:
                return BitConverter.ToUInt32(span);
            case DataType.Float:
            case DataType.TripleFloat:
                return BitConverter.ToSingle(span);
            case DataType.Double:
                return BitConverter.ToDouble(span);
            default:
                throw new InvalidOperationException("Unknown type");
            }
        }

        private static void SetValue<TPacketData>(IPacketParameter<TPacketData> parameter, byte[] buffer, int offset, object value)
            where TPacketData : PacketDataBase
        {
            try
            {
                if (parameter.NumBits > 0)
                {
                    // todo: ....
                    //var intValue = Convert.ToInt32(value);
                    //var mask = (1 << (int)parameter.NumBits) - 1;
                    //var shiftedToStart = intValue >> ((int)parameter.Type.BitCount() - (int)parameter.StartBit);
                    //value = shiftedToStart & mask;
                }

                SetValue(parameter.PacketType.ToDataType(), buffer, offset, parameter.ByteCount, value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(parameter), $"Error setting value of {parameter.Name}: {ex.Message}");
            }
        }

        private static void SetValue(DataType type, byte[] buffer, int offset, int count, object value)
        {
            byte[] bytes;
            switch (type)
            {
            case DataType.Char:
                bytes = ArrayExtensions.CreateArray((byte)Convert.ToSByte(value));
                break;
            case DataType.Packed:
                bytes = ArrayExtensions.CreateArray(Convert.ToByte(value));
                break;
            case DataType.Short:
                bytes = BitConverter.GetBytes(Convert.ToInt16(value));
                break;
            case DataType.UShort:
                bytes = BitConverter.GetBytes(Convert.ToUInt16(value));
                break;
            case DataType.Int:
            case DataType.LLong:
                bytes = BitConverter.GetBytes(Convert.ToInt32(value));
                break;
            case DataType.UInt:
                bytes = BitConverter.GetBytes(Convert.ToUInt32(value));
                break;
            case DataType.Float:
            case DataType.TripleFloat:
                bytes = BitConverter.GetBytes(Convert.ToSingle(value));
                break;
            case DataType.Double:
                bytes = BitConverter.GetBytes(Convert.ToDouble(value));
                break;
            default:
                throw new InvalidOperationException("Unknown type");
            }

            var span = buffer.AsSpan(offset, count);
            bytes.AsSpan().CopyTo(span);
        }
    }
}