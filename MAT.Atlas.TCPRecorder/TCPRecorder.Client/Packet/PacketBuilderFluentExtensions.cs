// <copyright file="PacketBuilderFluentExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System.Runtime.InteropServices;

namespace TCPRecorder.Client.Packet
{
    public static class PacketBuilderFluentExtensions
    {
        //public static PacketBuilderBase<TPacketData> Array<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder, int dimension) => packetBuilder.SetArray(dimension);

        public static PacketBuilderBase<TPacketData> Byte<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Byte);

        public static PacketBuilderBase<TPacketData> Char<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Char);

        public static PacketBuilderBase<TPacketData> Double<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Double);

        public static PacketBuilderBase<TPacketData> DWord<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.DWord);

        public static PacketBuilderBase<TPacketData> Float<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Float);

        public static PacketBuilderBase<TPacketData> Int<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Int);

        public static PacketBuilderBase<TPacketData> Int16<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Int16);

        public static PacketBuilderBase<TPacketData> Int32<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Int32);

        public static PacketBuilderBase<TPacketData> Int64<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder, int count = 1)
            where TPacketData : PacketDataBase => Add<TPacketData, long>(packetBuilder, count);

        public static PacketBuilderBase<TPacketData> Int8<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Int8);

        public static PacketBuilderBase<TPacketData> LLong<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.LLong);

        public static PacketBuilderBase<TPacketData> Long<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Int64(packetBuilder);

        public static PacketBuilderBase<TPacketData> LWord<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => UInt64(packetBuilder);

        public static PacketBuilderBase<TPacketData> Packed<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Packed);

        public static PacketBuilderBase<TPacketData> Padding<TPacketData>(PacketBuilderBase<TPacketData> packetBuilder, int byteCount)
            where TPacketData : PacketDataBase => packetBuilder.Add(byteCount);

        public static PacketBuilderBase<TPacketData> SByte<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.SByte);

        public static PacketBuilderBase<TPacketData> Short<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Short);

        public static PacketBuilderBase<TPacketData> SignedChar<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.SignedChar);

        public static PacketBuilderBase<TPacketData> SignedInt<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.SignedInt);

        public static PacketBuilderBase<TPacketData> SignedLong<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Int64(packetBuilder);

        public static PacketBuilderBase<TPacketData> SignedShort<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.SignedShort);

        public static PacketBuilderBase<TPacketData> Single<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Single);

        public static PacketBuilderBase<TPacketData> String<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder, int size)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.String, size);

        public static PacketBuilderBase<TPacketData> Struct<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder, PacketBuilderBase<TPacketData> structPacketBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, structPacketBuilder);

        public static int ToInt<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => (int)packetBuilder.ByteCount;

        public static PacketBuilderBase<TPacketData> TripleFloat<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.TripleFloat);

        public static PacketBuilderBase<TPacketData> UInt<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.UInt);

        public static PacketBuilderBase<TPacketData> UInt16<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.UInt16);

        public static PacketBuilderBase<TPacketData> UInt32<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.UInt32);

        public static PacketBuilderBase<TPacketData> UInt64<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder, int count = 1)
            where TPacketData : PacketDataBase => Add<TPacketData, ulong>(packetBuilder, count);

        public static PacketBuilderBase<TPacketData> UInt8<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.UInt8);

        public static PacketBuilderBase<TPacketData> ULong<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => UInt64(packetBuilder);

        public static PacketBuilderBase<TPacketData> UnsignedChar<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.UnsignedChar);

        public static PacketBuilderBase<TPacketData> UnsignedInt<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.UnsignedInt);

        public static PacketBuilderBase<TPacketData> UnsignedLong<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => UInt64(packetBuilder);

        public static PacketBuilderBase<TPacketData> UnsignedShort<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.UnsignedShort);

        public static PacketBuilderBase<TPacketData> UShort<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.UShort);

        public static PacketBuilderBase<TPacketData> Word<TPacketData>(this PacketBuilderBase<TPacketData> packetBuilder)
            where TPacketData : PacketDataBase => Add(packetBuilder, PacketFieldType.Word);

        private static PacketBuilderBase<TPacketData> Add<TPacketData>(PacketBuilderBase<TPacketData> packetBuilder, PacketFieldType packetType, int size = 1)
            where TPacketData : PacketDataBase => packetBuilder.Add(packetType, size);

        private static PacketBuilderBase<TPacketData> Add<TPacketData>(PacketBuilderBase<TPacketData> packetBuilder, PacketBuilderBase<TPacketData> structPacketBuilder)
            where TPacketData : PacketDataBase => packetBuilder.Add(structPacketBuilder);

        private static PacketBuilderBase<TPacketData> Add<TPacketData, T>(PacketBuilderBase<TPacketData> packetBuilder, int count)
            where TPacketData : PacketDataBase => packetBuilder.Add(Marshal.SizeOf<T>() * count);
    }
}