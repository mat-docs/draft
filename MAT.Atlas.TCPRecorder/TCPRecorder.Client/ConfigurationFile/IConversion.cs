// <copyright file="IConversion.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.ConfigurationFile
{
    public interface IConversion
    {
        string DefaultValue { get; set; }

        string Formula { get; set; }

        string Name { get; set; }

        string[] StringValues { get; set; }

        ConversionType Type { get; set; }

        double[] Values { get; set; }
    }
}