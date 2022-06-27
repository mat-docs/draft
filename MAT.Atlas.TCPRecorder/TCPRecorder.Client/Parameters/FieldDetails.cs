// <copyright file="FieldDetails.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System;

namespace TCPRecorder.Client.Parameters
{
    public abstract class FieldDetails
    {
        protected FieldDetails(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(Name), "Field name cannot be empty");
            }

            Name = name;
            Description = description ?? name;
        }

        public string Description { get; }

        public string Name { get; }

        public override string ToString() => Name;
    }
}