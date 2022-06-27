// <copyright file="Group.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System.Collections.Generic;

namespace TCPRecorder.Client.ConfigurationFile
{
    public sealed class Group
    {
        public Group()
            : this(null, null)
        {
        }

        public Group(Dictionary<string, Parameter> parameters)
            : this(null, parameters)
        {
        }

        public Group(Dictionary<string, Group> subGroups)
            : this(subGroups, null)
        {
        }

        public Group(Dictionary<string, Group> subGroups, Dictionary<string, Parameter> parameters)
        {
            this.SubGroups = subGroups ?? new Dictionary<string, Group>();
            this.Parameters = parameters ?? new Dictionary<string, Parameter>();
        }

        public Dictionary<string, Parameter> Parameters { get; set; }

        public Dictionary<string, Group> SubGroups { get; set; }
    }
}