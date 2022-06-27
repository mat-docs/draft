// <copyright file="DefaultPacketData.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

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