// <copyright file="Configuration.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

using Newtonsoft.Json;

using NLog;
using TCPRecorder.Client.Extensions;
using TcpRange = TCPRecorder.Client.ConfigurationFile.Range;

namespace TCPRecorder.Client.ConfigurationFile
{
    public sealed class ConfigurationFileOperations
    {
        public const uint ParametersStartByteOffset = 8;
        private const string RoamingConfigPath = @"McLaren Applied Technologies\ATLAS 10\Configuration\roaming.user.config";
        private const string ConfigRecordersFolderXPath =
            @"configuration/applicationConfiguration/categories/category[@name = 'Recorders']/subCategories/subCategory[@name = 'Folders']/settings/setting[@name = 'Folders']";
        private static readonly ByteOrderType ByteOrder = BitConverter.IsLittleEndian ? ByteOrderType.LittleEndian : ByteOrderType.BigEndian;
        private static readonly JsonConverter[] Converters = { new ConversionConverter() };
        private static readonly Logger NLogLogger = LogManager.GetCurrentClassLogger();
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings { Converters = Converters };

        public static Parameter CreateParameter(
            string name,
            string description,
            string conversionName,
            DataType parameterType,
            uint parameterOffset,
            double minimumValue,
            double maximumValue,
            string units,
            string format,
            double frequency,
            uint? numBits = null,
            uint startBit = default)
        {
            return new Parameter(
                name,
                description,
                conversionName,
                units,
                format,
                ByteOrder,
                new TcpRange(minimumValue, maximumValue),
                TcpRange.Zero,
                Signal.CreateSignals(
                    new Signal(
                        name,
                        parameterType,
                        DataSource.Periodic,
                        frequency,
                        parameterOffset,
                        numBits,
                        startBit)));
        }

        public static string GetConfigurationFileFolderPath()
        {
            var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var configPath = Path.Combine(myDocuments, RoamingConfigPath);
            if (!File.Exists(configPath))
            {
                NLogLogger.Error($"'{configPath}' is not found.");
                return string.Empty;
            }

            var configXml = XDocument.Load(configPath);
            var configurationFileFolderElement = configXml.XPathSelectElement(ConfigRecordersFolderXPath);
            var configurationFileFolderPath = configurationFileFolderElement?.Attribute("value")?.Value;
            if (string.IsNullOrWhiteSpace(configurationFileFolderPath))
            {
                NLogLogger.Error("Atlas 10 setting 'Tools/Options/Recorders/Folders/Configuration Folders' is not configured.");
                return string.Empty;
            }

            if (!Directory.Exists(configurationFileFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(configurationFileFolderPath);
                }
                catch (Exception ex)
                {
                    NLogLogger.Error(ex, $"Create Folder '{configurationFileFolderPath}' failed.");
                    return string.Empty;
                }
            }

            return configurationFileFolderPath;
        }

        public static SessionConfiguration LoadConfiguration(string fullPathToFile)
        {
            if (!File.Exists(fullPathToFile))
            {
                NLogLogger.Error("TCP recorder configuration file not found");
                return null;
            }

            try
            {
                var text = File.ReadAllText(fullPathToFile);
                return JsonConvert.DeserializeObject<SessionConfiguration>(text, Settings);
            }
            catch (Exception ex)
            {
                NLogLogger.Error(ex, "Error reading TCP recorder configuration file");
                return null;
            }
        }

        public static bool SaveConfiguration(
            SessionConfiguration sessionConfiguration,
            string configurationFileFolderPath,
            ref string configurationIdentifier,
            out string configurationFilePath)
        {
            configurationFilePath = string.Empty;

            try
            {
                var configurationText = JsonConvert.SerializeObject(sessionConfiguration, Settings);

                // Generate identifier from configuration itself?
                if (string.IsNullOrEmpty(configurationIdentifier))
                {
                    // Convert configuration text to bytes
                    var bytes = Encoding.Unicode.GetBytes(configurationText);

                    // Calculate MD5 of configuration bytes
                    var md5 = MD5.Create();
                    var hashBytes = md5.ComputeHash(bytes);

                    // Convert MD5 hash to string
                    configurationIdentifier = hashBytes.ToHex();

                    // Update configuration identifier and regenerate configuration text
                    sessionConfiguration.Configuration.Identifier = configurationIdentifier;
                    configurationText = JsonConvert.SerializeObject(sessionConfiguration, Settings);
                }

                if (!string.IsNullOrWhiteSpace(configurationFileFolderPath))
                {
                    var configurationFileName = Path.ChangeExtension(configurationIdentifier, "json");
                    configurationFilePath = Path.Combine(configurationFileFolderPath, configurationFileName);

                    File.WriteAllText(configurationFilePath, configurationText);
                }

                return true;
            }
            catch (Exception ex)
            {
                NLogLogger.Error(ex, "Error writing TCP recorder configuration file");
                configurationFilePath = null;
                return false;
            }
        }
    }
}