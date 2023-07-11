using System;

namespace LPRT
{
    public class PacketTimeLineEntry
    {
        private string _time;
        private string _position;
        private string _type;
        private String[] _rowdData;

        public PacketTimeLineEntry(string time, string position, string type)
        {
            _time = time;
            _position = position;
            _type = type;
            _rowdData = new string[] { _time, _position, _type };
        }
        
        public PacketTimeLineEntry()
        {
        }

        public string Time
        {
            get => _time;
            set => _time = value;
        }

        public string Position
        {
            get => _position;
            set => _position = value;
        }

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public string[] RowData
        {
            get => _rowdData;
            set => _rowdData = value;
        }
    }
}