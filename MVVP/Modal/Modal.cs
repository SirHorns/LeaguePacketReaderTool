using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using LPRT.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.Modal
{
    public class Modal : INotifyPropertyChanged
    {
        private string _filePath = "";
        private string _packetFilter = "";
        private JArray _rawData;
        private List<string> _packetTypes;
        private List<PacketTimeLineEntry> _packetTimeline;
        private List<PacketTimeLineEntry> _filteredPacketTimeline;
        private ListViewItem[] _timelineCache;
        
        private readonly JsonSerializer _serializer;

        public Modal(IModalCommands viewModal)
        {
            _serializer = new JsonSerializer();
            PropertyChanged += InternalPropertyChanged;
        }

        /// <summary>
        /// JArray object of the contents of the provided Match JSON file.
        /// </summary>
        private JArray RawData
        {
            get => _rawData;
            set
            {
                if(value == null) {return;}
                _rawData = value;
                OnPropertyChanged(nameof(this.RawData));
            }
        }
        /// <summary>
        /// String list containing the name of all Packet-types present within the match fiile.
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
        /// Filter used to creat Filtered Timeline.
        /// </summary>
        public string PacketFilter
        {
            get => _packetFilter;
            set
            {
                _packetFilter = value;
                OnPropertyChanged(nameof(this.PacketFilter));
            }
        }
        /// <summary>
        /// PacketTimelineEntry list containing the full timeline information within the RawData.
        /// </summary>
        public List<PacketTimeLineEntry> PacketTimeline
        {
            get => _packetTimeline;
            private set
            {
                _packetTimeline = value;
                OnPropertyChanged(nameof(this.PacketTimeline));
            }
        }
        /// <summary>
        /// PacketTimelineEntry list containing a filtered list of packets from the PacketTimeline.
        /// </summary>
        public List<PacketTimeLineEntry> FilteredPacketTimeline
        {
            get => _filteredPacketTimeline;
            set
            {
                _filteredPacketTimeline = value;
                OnPropertyChanged(nameof(this.FilteredPacketTimeline));
            }
        }

        public ListViewItem[] TimelineCache
        {
            get => _timelineCache;
            set
            {
                _timelineCache = value;
                OnPropertyChanged(nameof(this.TimelineCache));
            }
        }


        /// <summary>
        /// Loads the match file based on the file path into RawData
        /// </summary>
        private async void LoadMatchFile()
        {
            using (StreamReader sr = new StreamReader(FilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                try
                {
                    RawData = await Task.Run(() => _serializer.Deserialize<JArray>(reader));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private async void LoadCache()
        {
            using (StreamReader sr = new StreamReader(FilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                int cacheCap = 50;
                int index = 0;
                _timelineCache = new ListViewItem[cacheCap];
                while (reader.Read() && index <= cacheCap)
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var t = _serializer.Deserialize(reader);
                        JObject jObject = (JObject)t;
                        _timelineCache[0] = new ListViewItem(new []{ jObject["Packet"]["$type"].ToString(), "BAD", "W0LF" });
                    }
                    
                    index=+1;
                }
            }
        }
        
        
        /// <summary>
        /// Parses through _RawData pulling packet type names from the given match file.
        /// Triggers OnPropertyChanged.
        /// </summary>
        private void GetPacketTypes()
        {
            List<string> packetTypes = new List<string>();
            string type; 

            foreach (var jToken in RawData.Children())
            {
                type = GetPacketName(jToken["Packet"]["$type"].ToString());

                if (!packetTypes.Contains(type))
                {
                    packetTypes.Add(type); 
                }
            }
            packetTypes.Sort();
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
        /// Creates the Timeline of packets for the given match data.
        /// </summary>
        private void GetPacketTimeLine()
        {
            List<PacketTimeLineEntry> newTimeline = new List<PacketTimeLineEntry>();
            
            int index = 0;
            foreach (var jToken in RawData.Children())
            {
                newTimeline.Add(new PacketTimeLineEntry(jToken["Time"].ToString(), index.ToString(), GetPacketName(jToken["Packet"]["$type"].ToString())));
                index++;
            }
            
            PacketTimeline = newTimeline;
        }
        
        /// <summary>
        /// Creates a filtered Timeline of packets for the given match data.
        /// </summary>
        private void GetFilteredPacketTimeLine()
        {
            List<PacketTimeLineEntry> newTimeline = new List<PacketTimeLineEntry>();
            
            int index = 0;
            foreach (var jToken in RawData.Children())
            {
                string type = GetPacketName(jToken["Packet"]["$type"].ToString());

                if (type.Equals(PacketFilter))
                {
                    newTimeline.Add(new PacketTimeLineEntry(
                        jToken["Time"].ToString(),
                        index.ToString(),
                        type));
                }
                
                index++;
            }
            FilteredPacketTimeline = newTimeline;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        public ListViewItem GetTimelineEntryValue(int itemIndex)
        {
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.

            //check to see if the requested item is currently in the cache
            if (myCache != null && itemIndex >= firstItem && itemIndex < firstItem + myCache.Length)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                return myCache[itemIndex - firstItem];
            }
            else
            {
                //A cache miss, so create a new ListViewItem and pass it back.
                int x = itemIndex * itemIndex;
                return new ListViewItem(x.ToString());
            }
        }

        /// <summary>
        /// Returns the packet info within RawData based on packet position.
        /// </summary>
        /// <param name="index">Position of the packet within the timeline.</param>
        /// <returns>List of String arrays containing the contents of a packet instance.</returns>
        public List<string[]> GetPacketInfo(int index)
        {
            List<string[]> data = new List<string[]>();
            string[] row;
            
            var packet = RawData[index]["Packet"] as JObject;
            
            if (packet == null)
            {
                row = new string[2];
                row[0] = "BAD";
                row[1] = "WOLF";
                data.Add(row);
                
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
        /// Returns the packet raw JSON info within RawData based on packet position.
        /// </summary>
        /// <param name="index">Position of the packet within the timeline.</param>
        /// <returns>Returns the raw JSON string of a packet</returns>
        public string GetRawPacketInfo(int index)
        {
            var packet = _rawData[index]["Packet"] as JObject;
            return packet == null ? "{\"BADW0LF\": \"\"}" : packet.ToString();
        }

        private ListViewItem[] myCache; //array to cache items for the virtual list
        private int firstItem; //stores the index of the first item in the cache
        public void RebuildCache(int startIndex, int endIndex)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really neccesary.
            if (myCache != null && startIndex >= firstItem && endIndex <= firstItem + myCache.Length)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //Now we need to rebuild the cache.
            firstItem = startIndex;
            int length = endIndex - startIndex + 1; //indexes are inclusive
            myCache = new ListViewItem[length];

            //Fill the cache with the appropriate ListViewItems.
            int x = 0;

            for (int i = 0; i < length; i++)
            {
                x = (i + firstItem) * (i + firstItem);

                ListViewItem item = new ListViewItem(new []{ x.ToString(), x.ToString(), x.ToString() });
                myCache[i] =  item;
            }
        }

        private void PopulateCache(int length)
        {
            //Fill the cache with the appropriate ListViewItems.
            int x = 0;
            for (int i = 0; i < length; i++)
            {
                PacketTimeLineEntry entry = new PacketTimeLineEntry(
                    RawData[i]["Packet"]["$type"].ToString(),
                    RawData[i]["time"].ToString(),
                    RawData[i]["time"].ToString()
                );
                x = (i + firstItem) * (i + firstItem);

                ListViewItem item = new ListViewItem(new []{ entry.Type, entry.Position, entry.Time });
                myCache[i] =  item;
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        /// <summary>
        /// Handles internal property change events.
        /// </summary>
        private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FilePath":
                    //LoadMatchFile();
                    LoadCache();
                    break;
                case "PacketFilter":
                    GetFilteredPacketTimeLine();
                    break;
                case "RawData":
                    GetPacketTypes();
                    GetPacketTimeLine();
                    break;
                case "PacketTypes":
                    break;
                case "PacketTimeLine":
                    break;
                case "FilteredPacketTimeLine":
                    break;
            }
        }
    }
    
    //ඞ Amogus?
}