// <copyright file="DefaultPacketData.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

namespace TCPRecorder.Client.Packet
{
    public sealed class DefaultPacketData : PacketDataBase
    {
        public DefaultPacketData(int? packetId, byte[] sourcePacket, byte[] targetPacket)
            : base(packetId, sourcePacket, targetPacket)
        {
        }
    }
}