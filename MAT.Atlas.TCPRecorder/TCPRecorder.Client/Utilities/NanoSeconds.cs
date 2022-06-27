// <copyright file="NanoSeconds.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Runtime.CompilerServices;

namespace TCPRecorder.Client.Utilities
{
    public sealed class NanoSeconds
    {
        public NanoSeconds(long nanoSeconds)
        {
            this.Value = nanoSeconds;
        }

        public double Seconds
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.Value / (double)TimeUtilities.NanosecondsPerSecond;
        }

        public long Ticks
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.Value / TimeUtilities.NanosecondsPerTick;
        }

        public long Value { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NanoSeconds AlignTicks(long initialValueInTicks, NanoSeconds interval)
        {
            var initialValueInNanoseconds = initialValueInTicks * TimeUtilities.NanosecondsPerTick;
            return new NanoSeconds((long)(Math.Ceiling(initialValueInNanoseconds / (double)interval.Value) * interval.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NanoSeconds operator +(NanoSeconds lhs, NanoSeconds rhs)
        {
            return new NanoSeconds(lhs.Value + rhs.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NanoSeconds operator -(NanoSeconds lhs, NanoSeconds rhs)
        {
            return new NanoSeconds(lhs.Value - rhs.Value);
        }
    }
}