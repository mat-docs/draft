// <copyright file="NanosecondsExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;

namespace TCPRecorder.Client.Extensions
{
    public static class NanosecondsExtensions
    {
        public static long ToNanoseconds(this TimeSpan timeSpan)
        {
            return timeSpan.Ticks * 100;
        }

        public static string ToTimeString(this long nanoseconds)
        {
            if (nanoseconds < 0)
            {
                return '-' + TimeSpan.FromTicks(-nanoseconds / 100).ToString();
            }

            return TimeSpan.FromTicks(nanoseconds / 100).ToString();
        }
    }
}