// <copyright file="Configuration.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.ConfigurationFile
{
    public sealed class Configuration
    {
        public Configuration()
        {
        }

        public Configuration(string identifier, string version)
        {
            this.Identifier = identifier;
            this.Version = version;
        }

        public string Identifier { get; set; }

        public string Version { get; set; }
    }
}