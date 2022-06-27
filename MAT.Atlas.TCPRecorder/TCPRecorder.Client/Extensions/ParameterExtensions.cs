// <copyright file="ParameterExtensions.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System;
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

        public static void SetValue<TPacketData>(this IPacketParameter<TPacketData> parameter, byte[] target, object value)
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