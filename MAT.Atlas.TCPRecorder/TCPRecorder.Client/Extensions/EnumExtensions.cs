// <copyright file="EnumExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace TCPRecorder.Client.Extensions
{
    public static class EnumExtensions
    {
        public static IReadOnlyList<Tuple<string, T>> GetChoices<T>()
            where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(v => Tuple.Create(v.GetName(), v)).ToList();
        }

        public static string GetName(this Enum value)
        {
            var enumType = value.GetType();
            var name = value.ToString();
            var enumMembers = enumType.GetMember(name);
            var descriptionAttribute = enumMembers.First().GetCustomAttribute<DescriptionAttribute>();
            return descriptionAttribute?.Description ?? name;
        }
    }
}