// <copyright file="ArrayExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;

namespace TCPRecorder.Client.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] CreateArray<T>(params T[] values)
        {
            return values;
        }

        public static TOutput[] CreateArray<TInput, TOutput>(Converter<TInput, TOutput> converter, params TInput[] values)
        {
            return Array.ConvertAll(values, converter);
        }
    }
}