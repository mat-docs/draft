// <copyright file="PacketConfiguration.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

using System.Collections.Generic;
using System.Linq;

using NLog;

using TCPRecorder.Client.ConfigurationFile;
using TCPRecorder.Client.Extensions;
using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client
{
    public static class PacketConfiguration
    {
        private const string ConversionName = "Simple1to1";
        private static readonly Logger NLogLogger = LogManager.GetCurrentClassLogger();

        public static bool GenerateConfiguration<TPacketData>(
            ref string configurationIdentifier,
            int configurationVersion,
            string defaultApplicationGroup,
            IEnumerable<IPacketParameter<TPacketData>> packetParameters,
            out int tcpPacketSize,
            double frequency = 60)
            where TPacketData : PacketDataBase
        {
            tcpPacketSize = 0;

            var conversion = RationalConversion.CreateSimple1To1Conversion(ConversionName);

            var groups = new Dictionary<string, Group>();
            var parameterSourceOffset = 0;
            var parameterTargetOffset = 0;
            var packetId = default(int?);
            foreach (var packetParameter in packetParameters)
            {
                if (packetParameter.GetValue != null)
                {
                    packetParameter.SetPacketId(null);
                }
                else
                {
                    if (packetParameter.PacketId != packetId)
                    {
                        if (packetParameter.PacketId.HasValue)
                        {
                            packetId = packetParameter.PacketId;
                        }
                        else
                        {
                            packetParameter.SetPacketId(packetId);
                        }
                    }

                    if (packetParameter.SourceByteOffset < 0)
                    {
                        packetParameter.SetSourceByteOffset(parameterSourceOffset);
                    }
                    else
                    {
                        parameterSourceOffset = packetParameter.SourceByteOffset;
                    }

                    parameterSourceOffset += packetParameter.ByteCount;
                }

                if (packetParameter.TargetByteOffset < 0)
                {
                    packetParameter.SetTargetByteOffset(parameterTargetOffset);
                }
                else
                {
                    parameterTargetOffset = packetParameter.TargetByteOffset;
                }

                if (string.IsNullOrWhiteSpace(packetParameter.Name))
                {
                    NLogLogger.Error("Generating configuration file failed: parameter name cannot be empty");
                    return false;
                }

                var dataType = packetParameter.PacketType.ToDataType();
                var parameter = ConfigurationFileOperations.CreateParameter(
                    packetParameter.Name,
                    packetParameter.Description ?? packetParameter.Name,
                    conversion.Name,
                    dataType,
                    (uint)parameterTargetOffset + ConfigurationFileOperations.ParametersStartByteOffset,
                    packetParameter.Minimum,
                    packetParameter.Maximum,
                    packetParameter.Units,
                    packetParameter.Format ?? dataType.DefaultFormat(),
                    frequency,
                    packetParameter.NumBits,
                    packetParameter.StartBit);

                var groupPath = new List<string>();

                var applicationGroup = defaultApplicationGroup;
                if (!string.IsNullOrWhiteSpace(packetParameter.ApplicationGroup))
                {
                    applicationGroup = packetParameter.ApplicationGroup;
                }

                groupPath.Add(applicationGroup);

                if (packetParameter.GroupPath != null &&
                    packetParameter.GroupPath.Any())
                {
                    groupPath.AddRange(packetParameter.GroupPath);
                }

                var group = GetGroup(groups, groupPath);

                group.Parameters[parameter.Name] = parameter;
                parameterTargetOffset += packetParameter.ByteCount;
            }

            tcpPacketSize = parameterTargetOffset;

            var sessionConfiguration = new SessionConfiguration(
                new Configuration(configurationIdentifier, configurationVersion.ToString()),
                groups,
                new Dictionary<string, IConversion> { [conversion.Name] = conversion });

            var configurationFileFolderPath = ConfigurationFileOperations.GetConfigurationFileFolderPath();
            if (!ConfigurationFileOperations.SaveConfiguration(
                sessionConfiguration,
                configurationFileFolderPath,
                ref configurationIdentifier,
                out var configurationFilePath))
            {
                NLogLogger.Error("Writing configuration file failed!");
                return false;
            }
#if DEBUG
            // Ensure configuration can be re-read
            if (!string.IsNullOrWhiteSpace(configurationFilePath) &&
                ConfigurationFileOperations.LoadConfiguration(configurationFilePath) == null)
            {
                NLogLogger.Error("Reading configuration file failed!");
                return false;
            }
#endif
            return true;
        }

        private static Group GetGroup(IDictionary<string, Group> groups, IEnumerable<string> groupPath)
        {
            var group = default(Group);
            foreach (var groupName in groupPath)
            {
                if (!groups.TryGetValue(groupName, out group))
                {
                    group = new Group();
                    groups.Add(groupName, group);
                }

                groups = group.SubGroups;
            }

            return group;
        }
    }
}