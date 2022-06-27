// <copyright file="ArrayExtensions.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] CreateArray<T>(params T[] values)
        {
            return values;
        }
    }
}