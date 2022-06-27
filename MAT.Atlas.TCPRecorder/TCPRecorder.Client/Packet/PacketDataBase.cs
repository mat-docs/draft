// <copyright file="PacketDataBase.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.Packet
{
    public abstract class PacketDataBase
    {
        protected PacketDataBase()
        {
        }

        protected PacketDataBase(int? packetId, byte[] sourcePacket, byte[] targetPacket)
        {
            this.Set(packetId, sourcePacket, targetPacket);
        }

        public int? PacketId { get; private set; }

        public byte[] SourcePacket { get; private set; }

        public byte[] TargetPacket { get; private set; }

        public void Set(int? packetId, byte[] sourcePacket, byte[] targetPacket)
        {
            this.PacketId = packetId;
            this.SourcePacket = sourcePacket;
            this.TargetPacket = targetPacket;
        }
    }
}