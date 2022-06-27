// <copyright file="Signal.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.ConfigurationFile
{
    public enum TriggerSource : byte
    {
        PitStraight = 0,

        PitLane = 1,

        Default = 2,

        Start = 3,

        End = 4,

        Unknown = 5
    }
}