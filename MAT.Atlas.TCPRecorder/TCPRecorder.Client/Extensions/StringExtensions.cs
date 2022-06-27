// <copyright file="StringExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System.Collections.Generic;
using System.Text;

namespace TCPRecorder.Client.Extensions
{
    public static class StringExtensions
    {
        public static void AppendValues(this StringBuilder text, params object[] values)
        {
            AppendValues(text, (IReadOnlyList<object>)values);
        }

        public static void AppendValues(this StringBuilder text, IReadOnlyList<object> values)
        {
            for (var i = 0; i < values.Count; ++i)
            {
                if (i > 0)
                {
                    text.Append(",");
                }

                var valueString = values[i].ToString().Trim().Replace(',', '_');
                text.Append(valueString);
            }

            text.AppendLine();
        }
    }
}