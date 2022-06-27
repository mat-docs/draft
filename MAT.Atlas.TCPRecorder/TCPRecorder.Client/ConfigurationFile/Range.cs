// <copyright file="Range.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System;

namespace TCPRecorder.Client.ConfigurationFile
{
    public struct Range
    {
        public static readonly Range Zero = new Range();

        public Range(double min, double max)
        {
            this.Min = Math.Min(min, max);
            this.Max = Math.Max(min, max);
        }

        public double Max { get; set; }

        public double Min { get; set; }
    }
}