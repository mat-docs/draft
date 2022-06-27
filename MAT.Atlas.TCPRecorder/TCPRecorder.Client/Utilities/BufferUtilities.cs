// <copyright file="BufferUtilities.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System.Runtime.InteropServices;

namespace TCPRecorder.Client.Utilities
{
    public static class BufferUtilities
    {
        public static byte[] ToBytes<T>(T value)
            where T : struct
        {
            var memorySize = Marshal.SizeOf(value);
            var memory = Marshal.AllocHGlobal(memorySize);
            try
            {
                Marshal.StructureToPtr(value, memory, false);
                var buffer = new byte[memorySize];
                Marshal.Copy(memory, buffer, 0, memorySize);
                return buffer;
            }
            finally
            {
                Marshal.FreeHGlobal(memory);
            }
        }
    }
}