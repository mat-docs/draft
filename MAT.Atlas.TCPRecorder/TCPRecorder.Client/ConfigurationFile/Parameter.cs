// <copyright file="Parameter.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System.Collections.Generic;

using Newtonsoft.Json;

namespace TCPRecorder.Client.ConfigurationFile
{
    public sealed class Parameter
    {
        public Parameter(
            string name,
            string description,
            string conversionName,
            string units,
            string format,
            ByteOrderType byteOrder,
            Range physicalRange,
            Range warningRange,
            List<Signal> signals)
        {
            this.Name = name;
            this.Description = description;
            this.ConversionName = conversionName;
            this.Units = units;
            this.Format = format;
            this.ByteOrder = byteOrder.ToString();
            this.PhysicalRange = physicalRange;
            this.WarningRange = warningRange;
            this.Signals = signals;
        }

        public string ByteOrder { get; set; }

        [JsonProperty("Conversion")]
        public string ConversionName { get; set; }

        public string Description { get; set; }

        public string Format { get; set; }

        public string Name { get; set; }

        public Range PhysicalRange { get; set; }

        public List<Signal> Signals { get; set; }

        public string Units { get; set; }

        public Range WarningRange { get; set; }
    }
}