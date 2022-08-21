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
        
        private List<ListViewItem> _timelineCache;
        private int firstItem; //stores the index of the first item in the cache

        private readonly JsonSerializer _serializer;
        
        private int _timeLineSize = 50;

        public Modal(IModalCommands viewModal)
        {
            _serializer = new JsonSerializer();
            PacketTimeline = new List<PacketTimeLineEntry>();
            PropertyChanged += InternalPropertyChanged;
        }
        
        
        
        private JArray RawData
        {
            get => _rawData;
            set
            {
                if (value == null)
                {
                    return;
                }

                _rawData = value;
                OnPropertyChanged();
            }
        }
        public List<string> PacketTypes
        {
            get => _packetTypes;
            private set
            {
                if (value.Count() < 0)
                {
                    return;
                }

                _packetTypes = value;
                OnPropertyChanged();
            }
        }
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(this.FilePath));
            }
        }
        public string PacketFilter
        {
            get => _packetFilter;
            set
            {
                _packetFilter = value;
                OnPropertyChanged();
            }
        }
        public List<PacketTimeLineEntry> PacketTimeline
        {
            get => _packetTimeline;
            private set
            {
                _packetTimeline = value;
                OnPropertyChanged();
            }
        }
        public List<PacketTimeLineEntry> FilteredPacketTimeline
        {
            get => _filteredPacketTimeline;
            set
            {
                _filteredPacketTimeline = value;
                OnPropertyChanged();
            }
        }
        public List<ListViewItem> TimelineCache
        {
            get => _timelineCache;
            set
            {
                _timelineCache = value;
                OnPropertyChanged();
            }
        }

        public int TimeLineSize
        {
            get => _timeLineSize;
            private set => _timeLineSize = value;
        }


        //PARSE
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
        private async void LoadCache()
        {
            using (StreamReader sr = new StreamReader(FilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                try
                {
                    int index = 0;
                    
                    TimelineCache = await Task.Run(() =>
                    {
                        List<ListViewItem> tempList = new List<ListViewItem>();
                        
                        while (reader.Read())
                        { 
                            if (reader.TokenType == JsonToken.StartObject) 
                            {
                                var t = _serializer.Deserialize(reader);
                                JObject jObject = (JObject)t;
                                tempList.Add(new ListViewItem(new[]
                                {
                                    GetPacketName(jObject["Packet"]["$type"].ToString()), 
                                    index.ToString(),
                                    jObject["Time"].ToString()
                                }));
                            }
                            
                            index++;
                        }

                        TimeLineSize = tempList.Count;
                        return tempList;
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        
        //NOTIFY
        
        

        //PUBLISH
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
        public string GetRawPacketInfo(int index)
        {
            var packet = _rawData[index]["Packet"] as JObject;
            return packet == null ? "{\"BADW0LF\": \"\"}" : packet.ToString();
        }
        //Virtual-TimeLine
        public ListViewItem Publish_TimelineEntry(int itemIndex)
        {
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.

            //check to see if the requested item is currently in the cache
            if (TimelineCache != null && itemIndex >= firstItem && itemIndex < firstItem + TimelineCache.Count)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                return TimelineCache[itemIndex - firstItem];
            }

            //A cache miss, so create a new ListViewItem and pass it back.
            return new ListViewItem(new[] { "", "", "" });
        }
        public void Publish_CacheRebuild(int startIndex, int endIndex)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really neccesary.
            if (TimelineCache != null && startIndex >= firstItem && endIndex <= firstItem + TimelineCache.Count)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //Now we need to rebuild the cache.
            firstItem = startIndex;
        }


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
        private string GetPacketName(string packetType)
        {
            var split = packetType.Split('.');
            var packetName = split[split.Length - 1].Split(',')[0];

            return packetName;
        }
        private void GetPacketTimeLine()
        {
            List<PacketTimeLineEntry> newTimeline = new List<PacketTimeLineEntry>();

            int index = 0;
            foreach (var jToken in RawData.Children())
            {
                newTimeline.Add(new PacketTimeLineEntry(jToken["Time"].ToString(), index.ToString(),
                    GetPacketName(jToken["Packet"]["$type"].ToString())));
                index++;
            }

            PacketTimeline = newTimeline;
        }
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

                ListViewItem item = new ListViewItem(new[] { entry.Type, entry.Position, entry.Time });
                TimelineCache[i] = item;
            }
        }


        //PROPERTY-CHANGES
        private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FilePath":
                    //LoadMatchFile();
                    LoadCache();
                    break;
                case "PacketFilter":
                    break;
                case "RawData":
                    break;
                case "PacketTypes":
                    break;
                case "PacketTimeLine":
                    break;
                case "FilteredPacketTimeLine":
                    break;
                case "TimelineCache":
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    //ඞ Amogus?
}