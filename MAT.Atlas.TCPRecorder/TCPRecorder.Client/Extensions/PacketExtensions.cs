// <copyright file="PacketExtensions.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System;
using System.Runtime.InteropServices;

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