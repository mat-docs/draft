// <copyright file="FieldDetails.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;

namespace TCPRecorder.Client
{
    public abstract class FieldDetails
    {
        protected FieldDetails(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(this.Name), "Field name cannot be empty");
            }

            this.Name = name;
            this.Description = description ?? name;
        }

        public string Description { get; }

        public string Name { get; }

        public override string ToString() => this.Name;
    }
}