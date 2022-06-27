// <copyright file="PacketExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client.Extensions
{
    public static class PacketExtensions
    {
        public static uint BitCount(this PacketFieldType packetFieldType) => packetFieldType.ByteCount() * 8u;

        public static uint ByteCount(this PacketFieldType packetFieldType, uint size = 1)
        {
            var type = packetFieldType.ToType();
            var byteCount = (uint)(packetFieldType == PacketFieldType.String ? sizeof(byte) : Marshal.SizeOf(type));
            return byteCount * size;
        }

        public static int GetPacketSize(params Type[] types)
        {
            return types.Sum(Marshal.SizeOf);
        }

        public static object GetString(this byte[] buffer, int offset, int size) => Encoding.UTF8.GetString(buffer, offset, size);

        public static T GetValue<T>(this byte[] buffer, int offset) => (T)buffer.GetValue(typeof(T), offset);

        public static object GetValue(this byte[] buffer, Type type, int offset)
        {
            var byteCount = Marshal.SizeOf(type);

            if (offset < 0 ||
                offset >= buffer.Length ||
                offset + byteCount > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(offset),
                    $"{offset}+{byteCount} is outside of {buffer.Length}");
            }

            try
            {
                var span = buffer.AsSpan(offset, byteCount);

                if (type == typeof(bool))
                {
                    return BitConverter.ToBoolean(span);
                }

                if (type == typeof(char))
                {
                    return BitConverter.ToChar(span);
                }

                if (type == typeof(byte))
                {
                    return buffer[offset];
                }

                if (type == typeof(sbyte))
                {
                    return (sbyte)buffer[offset];
                }

                if (type == typeof(ushort))
                {
                    return BitConverter.ToUInt16(span);
                }

                if (type == typeof(short))
                {
                    return BitConverter.ToInt16(span);
                }

                if (type == typeof(uint))
                {
                    return BitConverter.ToUInt32(span);
                }

                if (type == typeof(int))
                {
                    return BitConverter.ToInt32(span);
                }

                if (type == typeof(ulong))
                {
                    return BitConverter.ToUInt64(span);
                }

                if (type == typeof(long))
                {
                    return BitConverter.ToInt64(span);
                }

                if (type == typeof(float))
                {
                    return BitConverter.ToSingle(span);
                }

                if (type == typeof(double))
                {
                    return BitConverter.ToDouble(span);
                }
            }
            catch
            {
                return default;
            }

            throw new InvalidOperationException("Unknown type");
        }

        public static double MaxValue(this PacketFieldType packetFieldType) => packetFieldType.ToType().MaxValue();

        public static double MaxValue(this Type type)
        {
            if (type == typeof(sbyte))
            {
                return sbyte.MaxValue;
            }

            if (type == typeof(byte))
            {
                return byte.MaxValue;
            }

            if (type == typeof(short))
            {
                return short.MaxValue;
            }

            if (type == typeof(ushort))
            {
                return ushort.MaxValue;
            }

            if (type == typeof(int))
            {
                return int.MaxValue;
            }

            if (type == typeof(uint))
            {
                return uint.MaxValue;
            }

            if (type == typeof(long))
            {
                return long.MaxValue;
            }

            if (type == typeof(ulong))
            {
                return ulong.MaxValue;
            }

            if (type == typeof(float))
            {
                return float.MaxValue;
            }

            if (type == typeof(double))
            {
                return double.MaxValue;
            }

            if (type == typeof(char))
            {
                return char.MaxValue;
            }

            if (type == typeof(string))
            {
                return 0;
            }

            throw new InvalidOperationException("Unknown Type");
        }

        public static double MinValue(this PacketFieldType packetFieldType) => packetFieldType.ToType().MinValue();

        public static double MinValue(this Type type)
        {
            if (type == typeof(sbyte))
            {
                return sbyte.MinValue;
            }

            if (type == typeof(byte))
            {
                return byte.MinValue;
            }

            if (type == typeof(short))
            {
                return short.MinValue;
            }

            if (type == typeof(ushort))
            {
                return ushort.MinValue;
            }

            if (type == typeof(int))
            {
                return int.MinValue;
            }

            if (type == typeof(uint))
            {
                return uint.MinValue;
            }

            if (type == typeof(long))
            {
                return long.MinValue;
            }

            if (type == typeof(ulong))
            {
                return ulong.MinValue;
            }

            if (type == typeof(float))
            {
                return float.MinValue;
            }

            if (type == typeof(double))
            {
                return double.MinValue;
            }

            if (type == typeof(char))
            {
                return char.MinValue;
            }

            if (type == typeof(string))
            {
                return 0;
            }

            throw new InvalidOperationException("Unknown Type");
        }

        public static void SetValue<T>(this byte[] buffer, int offset, T value)
        {
            var byteCount = Marshal.SizeOf<T>();

            if (offset < 0 ||
                offset >= buffer.Length ||
                offset + byteCount > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(offset),
                    $"{offset}+{byteCount} is outside of {buffer.Length}");
            }

            var type = typeof(T);

            byte[] bytes;
            if (type == typeof(bool))
            {
                bytes = BitConverter.GetBytes((bool)(object)value);
            }
            else if (type == typeof(char))
            {
                bytes = BitConverter.GetBytes((char)(object)value);
            }
            else if (type == typeof(byte))
            {
                bytes = ArrayExtensions.CreateArray((byte)(object)value);
            }
            else if (type == typeof(sbyte))
            {
                bytes = ArrayExtensions.CreateArray((byte)(sbyte)(object)value);
            }
            else if (type == typeof(ushort))
            {
                bytes = BitConverter.GetBytes((ushort)(object)value);
            }
            else if (type == typeof(short))
            {
                bytes = BitConverter.GetBytes((short)(object)value);
            }
            else if (type == typeof(uint))
            {
                bytes = BitConverter.GetBytes((uint)(object)value);
            }
            else if (type == typeof(int))
            {
                bytes = BitConverter.GetBytes((int)(object)value);
            }
            else if (type == typeof(ulong))
            {
                bytes = BitConverter.GetBytes((ulong)(object)value);
            }
            else if (type == typeof(long))
            {
                bytes = BitConverter.GetBytes((long)(object)value);
            }
            else if (type == typeof(float))
            {
                bytes = BitConverter.GetBytes((float)(object)value);
            }
            else if (type == typeof(double))
            {
                bytes = BitConverter.GetBytes((double)(object)value);
            }
            else
            {
                throw new InvalidOperationException("Unknown type");
            }

            var span = buffer.AsSpan(offset, byteCount);
            bytes.AsSpan().CopyTo(span);
        }

        public static Type ToType(this PacketFieldType packetFieldType)
        {
            switch (packetFieldType)
            {
            case PacketFieldType.Char:
            case PacketFieldType.SignedChar:
            case PacketFieldType.Int8:
            case PacketFieldType.SByte:
                return typeof(sbyte);
            case PacketFieldType.Packed:
            case PacketFieldType.UnsignedChar:
            case PacketFieldType.UInt8:
            case PacketFieldType.Byte:
                return typeof(byte);
            case PacketFieldType.Short:
            case PacketFieldType.SignedShort:
            case PacketFieldType.Int16:
                return typeof(short);
            case PacketFieldType.UShort:
            case PacketFieldType.UnsignedShort:
            case PacketFieldType.UInt16:
            case PacketFieldType.Word:
                return typeof(ushort);
            case PacketFieldType.Int:
            case PacketFieldType.SignedInt:
            case PacketFieldType.Int32:
            case PacketFieldType.LLong:
                return typeof(int);
            case PacketFieldType.UInt:
            case PacketFieldType.UnsignedInt:
            case PacketFieldType.UInt32:
            case PacketFieldType.DWord:
                return typeof(uint);
            case PacketFieldType.Float:
            case PacketFieldType.Single:
            case PacketFieldType.TripleFloat:
                return typeof(float);
            case PacketFieldType.Double:
                return typeof(double);
            case PacketFieldType.String:
                return typeof(string);
            default:
                throw new InvalidOperationException("Unknown Type");
            }
        }
    }
}