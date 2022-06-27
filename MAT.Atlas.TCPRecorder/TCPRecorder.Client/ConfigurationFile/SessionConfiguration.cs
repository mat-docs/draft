// <copyright file="SessionConfiguration.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System.Collections.Generic;

using Newtonsoft.Json;

namespace TCPRecorder.Client.ConfigurationFile
{
    public sealed class SessionConfiguration
    {
        public SessionConfiguration()
            : this(new Configuration(), new Dictionary<string, Group>(), new Dictionary<string, IConversion>())
        {
        }

        public SessionConfiguration(Configuration configuration, Dictionary<string, Group> applicationGroups, Dictionary<string, IConversion> conversions)
        {
            this.Configuration = configuration;
            this.ApplicationGroups = applicationGroups;
            this.Conversions = conversions;
        }

        [JsonProperty("ATLASParameters")]
        public Dictionary<string, Group> ApplicationGroups { get; set; }

        public Configuration Configuration { get; set; }

        public Dictionary<string, IConversion> Conversions { get; set; }
    }
}