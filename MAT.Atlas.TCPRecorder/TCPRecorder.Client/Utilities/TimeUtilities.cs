// <copyright file="TimeUtilities.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Runtime.CompilerServices;

namespace TCPRecorder.Client.Utilities
{
    public static class TimeUtilities
    {
        public const long NanosecondsPerSecond = 1000_000_000L;
        public const long NanosecondsPerTick = 100;
        public static long TicksPerMillisecond = 10_000L;
        public static long TicksPerSecond = 10_000_000L;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Format(TimeSpan timeSpan)
        {
            return timeSpan.ToString(@"hh\:mm\:ss\.fff");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Format(DateTime dateTime)
        {
            return dateTime.ToString("dd-MMM-yy HH:mm:ss.fff");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatSeconds(double seconds)
        {
            return Format(TimeSpan.FromSeconds(seconds));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatTicks(long ticks)
        {
            return Format(TimeSpan.FromTicks(ticks));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan GetNowSinceMidnight()
        {
            return DateTime.UtcNow - DateTime.Today.ToUniversalTime();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetNowSinceMidnightInSeconds()
        {
            return GetNowSinceMidnight().TotalSeconds;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetNowSinceMidnightInTicks()
        {
            return GetNowSinceMidnight().Ticks;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NanoSeconds IntervalInNanoseconds(double frequency)
        {
            return new NanoSeconds((long)((1 / frequency) * NanosecondsPerSecond));
        }
    }
}