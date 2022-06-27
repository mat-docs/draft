// <copyright file="PacketFieldType.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

namespace TCPRecorder.Client.Packet
{
    public enum PacketFieldType
    {
        // Signed 8Bit/1 Byte
        Char,
        SignedChar,
        Int8,
        SByte,

        // Unsigned 8Bit/1 Byte
        Packed,
        UnsignedChar,
        UInt8,
        Byte,

        // Signed 16Bit/2 Byte
        Short,
        SignedShort,
        Int16,

        // Unsigned 16Bit/2 Byte
        UShort,
        UnsignedShort,
        UInt16,
        Word,

        // Signed 32Bit/4 Byte
        Int,
        SignedInt,
        Int32,
        LLong,

        // Unsigned 32Bit/4 Byte
        UInt,
        UnsignedInt,
        UInt32,
        DWord,

        // Float 32Bit/4 Byte
        Float,
        Single,
        TripleFloat,

        // Double 64Bit/8 Byte
        Double,

        // String
        String
    }
}