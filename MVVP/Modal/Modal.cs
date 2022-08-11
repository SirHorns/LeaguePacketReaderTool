using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LPRT.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.Modal
{
    public class Modal : INotifyPropertyChanged
    {
        private IModalFunctions _viewModal;
        
        private string _filePath = "";
        private JArray _rawDataArray;
        private List<string> _packetTypes;
        private List<PacketTimeLineEntry> _packetTimelineEntries;
        
        private readonly JsonSerializer _serializer;

        public Modal(IModalFunctions viewModal)
        {
            _viewModal = viewModal;
            _serializer = new JsonSerializer();
            
            PropertyChanged += InternalPropertyChanged;
        }

        private JArray RawDataArray
        {
            get => _rawDataArray;
            set
            {
                if(value == null) {return;}
                _rawDataArray = value;
                OnPropertyChanged(nameof(this.RawDataArray));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> PacketTypes
        {
            get => _packetTypes;
            private set
            {
                if(value.Count() < 0){ return;}
                _packetTypes = value;
                OnPropertyChanged(nameof(this.PacketTypes));
            }
        }

        /// <summary>
        /// Directory path to json file given by the UI.
        /// Triggers OnPropertyChanged.
        /// </summary>
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(this.FilePath));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<PacketTimeLineEntry> PacketTimelineEntries
        {
            get => _packetTimelineEntries;
            private set
            {
                _packetTimelineEntries = value;
                OnPropertyChanged(nameof(this.PacketTimelineEntries));
            }
        }

        /// <summary>
        /// Loads the match file based on the file path.
        /// Called when _filePath is changed.
        /// </summary>
        private async void LoadMatchFile()
        {
            using (StreamReader sr = new StreamReader(FilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                try
                {
                    RawDataArray = await Task.Run((() => _serializer.Deserialize<JArray>(reader)));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        /// <summary>
        /// Parses through _rawDataArray pulling packet type names from the given match file.
        /// Triggers OnPropertyChanged.
        /// </summary>
        private void GetPacketTypes()
        {
            List<string> packetTypes = new List<string>();
            string type; 

            foreach (var jToken in RawDataArray.Children())
            {
                type = GetPacketName(jToken["Packet"]["$type"].ToString());

                if (!packetTypes.Contains(type))
                {
                    packetTypes.Add(type); 
                }
            }
            
            PacketTypes = packetTypes;
        }

        /// <summary>
        /// Parses the $type token and pulls out only the packet type.
        /// </summary>
        /// <param name="packetType">Raw json value of $type</param>
        /// <returns>Name of the Packet.</returns>
        private string GetPacketName(string packetType)
        {
            var split = packetType.Split('.');
            var packetName = split[split.Length - 1].Split(',')[0];
            
            return packetName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void GetPacketTimeLine()
        {
            List<PacketTimeLineEntry> timeLineList = new List<PacketTimeLineEntry>();
            
            int index = 0;
            foreach (var jToken in RawDataArray.Children())
            {
                timeLineList.Add(new PacketTimeLineEntry(
                    jToken["Time"].ToString(),
                    index.ToString(),
                    GetPacketName(jToken["Packet"]["$type"].ToString())));
                index++;
            }
            PacketTimelineEntries = timeLineList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<string[]> GetPacketInfo(int index)
        {
            List<string[]> data = new List<string[]>();
            string[] row;
            
            var packet = RawDataArray[index + 1]["Packet"] as JObject;
            
            if (packet == null)
            {
                row = new string[2];
                row[0] = "BAD";
                row[1] = "WOLF";
                
                return data;
            }
            
            foreach (KeyValuePair<string, JToken> pair in packet)
            {
                row = new string[2];
                row[0] = pair.Key;
                row[1] = pair.Value.ToString();
                data.Add(row);
            }
            
            return data;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetRawPacketInfo(int index)
        {
            var packet = _rawDataArray[index + 1]["Packet"] as JObject;
            return packet == null ? "{\"BADW0LF\": \"\"}" : packet.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FilePath":
                    LoadMatchFile();
                    break;
                case "RawDataArray":
                    GetPacketTypes();
                    GetPacketTimeLine();
                    break;
                case "PacketTypes":
                    break;
            }
        }
    }
    
    /*
     * ඞ
     */
}