using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.Modal
{
    public class ReplayPacket
    {
        private int _position;
        private string _rawId;
        private JObject _packet;
        private string _time;
        private string _channelId;
        private string _rawChannel;

        public ReplayPacket( string rawId, JObject packet, string time, string channelId, string rawChannel)
        {
            RawId = rawId;
            Packet = packet;
            Time = time;
            _channelId = channelId;
            RawChannel = rawChannel;
        }

        public int Position
        {
            get => _position;
            set => _position = value;
        }

        public string RawId
        {
            get => _rawId;
            private set => _rawId = value;
        }

        public JObject Packet
        {
            get => _packet;
            private set => _packet = value;
        }

        public string Time
        {
            get => _time;
            private set => _time = value;
        }

        public string ChannelId
        {
            get => _channelId;
            private set => _channelId = value;
        }

        public string RawChannel
        {
            get => _rawChannel;
            private set => _rawChannel = value;
        }
    }
}