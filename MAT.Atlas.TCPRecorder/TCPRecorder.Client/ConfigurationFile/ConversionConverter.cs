// <copyright file="ConversionConverter.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TCPRecorder.Client.ConfigurationFile
{
    internal sealed class ConversionConverter : JsonConverter
    {
        private static readonly string RationalConversionType = ConversionType.Rational.ToString().ToLowerInvariant();

        public override bool CanConvert(Type objectType)
        {
            return typeof(IConversion).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            if (jo["Type"].Value<string>() == RationalConversionType)
            {
                return jo.ToObject<RationalConversion>();
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is RationalConversion)
            {
                var jo = JObject.FromObject(value);
                jo["Type"] = RationalConversionType;
                jo.WriteTo(writer);
            }
        }
    }
}