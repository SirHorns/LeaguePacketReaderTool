using System;
using System.Collections.Generic;

namespace LPRT.MVVP.Modal
{
    public class Packet
    {
        private string _name;
        private string _id;
        private string _channel;
        private List<string> _keys;

        public Packet(string name, string id, string channel)
        {
            _name = name;
            _id = id;
            _channel = channel;
            _keys = new List<string>();
        }

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Id
        {
            get => _id;
            set => _id = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Channel
        {
            get => _channel;
            set => _channel = value ?? throw new ArgumentNullException(nameof(value));
        }

        public List<string> Keys
        {
            get => _keys;
            set => _keys = value ?? throw new ArgumentNullException(nameof(value));
        } 
    }
}