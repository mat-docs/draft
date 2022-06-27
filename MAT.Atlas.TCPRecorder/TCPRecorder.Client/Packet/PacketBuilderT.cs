// <copyright file="PacketBuilderT.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.Packet
{
    public sealed class PacketBuilderT<TPacketData> : PacketBuilderBase<TPacketData>
        where TPacketData : PacketDataBase
    {
        public PacketBuilderT(IParameterGroup parameterGroup = null)
            : base(parameterGroup)
        {
        }

        public PacketBuilderT(int? packetId, IParameterGroup parameterGroup = null)
            : base(packetId, parameterGroup)
        {
        }

        public PacketBuilderT(int? packetId, IApplicationGroup applicationGroup, IParameterGroup parameterGroup = null)
            : base(packetId, applicationGroup, parameterGroup)
        {
        }
    }
}