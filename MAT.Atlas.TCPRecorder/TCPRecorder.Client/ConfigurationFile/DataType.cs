// <copyright file="DataType.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.ConfigurationFile
{
    public enum DataType
    {
        Packed, // Unsigned 8Bit/1 Byte
        Char, // Signed 8Bit/1 Byte
        Short, // Signed 16Bit/2 Byte
        Int, // Signed 32Bit/4 Byte
        LLong, // Signed 32Bit/4 Byte
        Float, // Float 32Bit/4 Byte
        Double, // Double 64Bit/8 Byte
        UShort, // Unsigned 16Bit/2 Byte
        UInt, // Unsigned 32Bit/4 Byte
        TripleFloat, // Triple Float 32Bit/4 Byte
    }
}