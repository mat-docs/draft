// <copyright file="Conversion.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.ConfigurationFile
{
    public abstract class Conversion : IConversion
    {
        protected Conversion(
            string name,
            ConversionType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string DefaultValue { get; set; }

        public string Formula { get; set; }

        public string Name { get; set; }

        public string[] StringValues { get; set; }

        public ConversionType Type { get; set; }

        public double[] Values { get; set; }
    }
}