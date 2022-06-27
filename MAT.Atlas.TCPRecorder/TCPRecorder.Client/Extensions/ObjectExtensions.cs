// <copyright file="ObjectExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

namespace TCPRecorder.Client.Extensions
{
    public static class ObjectExtensions
    {
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}