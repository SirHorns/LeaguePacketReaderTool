using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using LPRT.MVVP.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.Modal
{
    public class Modal : INotifyPropertyChanged
    {
        private string _filePath;
        private string _timelineFilter;
        private string _selectedPlayer;
        private List<string> _packetTypes;
        
        private List<string> _jsons = new List<string>();
        private Teams _matchTeams = new Teams();
        
        private List<ListViewItem> _timelineCache;
        private List<ListViewItem> _tempTimelineCache;
        private List<ListViewItem> _filteredTimelineCache;
        private int _cacheFirstIndex; //stores the index of the first item in the cache

        private readonly JsonSerializer _serializer;
        

        public Modal()
        {
            _serializer = new JsonSerializer();
            PropertyChanged += InternalPropertyChanged;
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
                OnPropertyChanged();
            }
        }
        public string TimelineFilter
        {
            get => _timelineFilter;
            set
            {
                _timelineFilter = value;
                OnPropertyChanged();
            }
        }

        public string SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                _selectedPlayer = value;
                OnPropertyChanged();
            }
        }

        public List<ListViewItem> FilteredTimelineCache
        {
            get => _filteredTimelineCache;
            set
            {
                _filteredTimelineCache = value;
                OnPropertyChanged();
            }
        }
        private List<ListViewItem> TimelineCache
        {
            get => _timelineCache;
            set
            {
                _timelineCache = value;
                OnPropertyChanged();
            }
        }

        public Teams MatchTeams
        {
            get => _matchTeams;
            set
            {
                _matchTeams = value;
                OnPropertyChanged();
            }
        }

        public int TimeLineSize { get; private set; } = 50;


        //PARSE
        private async void LoadCache()
        {
            using (StreamReader sr = new StreamReader(FilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                try
                {
                    int index = 0;
                    
                    TimelineCache = await Task.Run(() => {
                        List<ListViewItem> tempList = new List<ListViewItem>();
                        
                        while (reader.Read())
                        { 
                            if (reader.TokenType == JsonToken.StartObject) 
                            {
                                var t = _serializer.Deserialize(reader);
                                JObject jObject = (JObject)t;
                                _jsons.Add(jObject.ToString());
                                tempList.Add(new ListViewItem(new[] { GetPacketName(jObject["Packet"]["$type"].ToString()), index.ToString(), jObject["Time"].ToString() }));
                            }
                            index++;
                        }
                        
                        TimeLineSize = tempList.Count;
                        return tempList;
                    });
                    _tempTimelineCache = TimelineCache;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            
            LoadPacketTypes();
            LoadPlayerInfo();
        }
        private async void LoadPacketTypes()
        {
            PacketTypes = await Task.Run(() => {
                var bag = new ConcurrentBag<string>();
                
                
                Parallel.ForEach(_jsons, str =>
                {
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.05) * 2.0))
                    };
                    
                    using (StringReader sr  = new StringReader(str))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        var token = _serializer.Deserialize(reader);
                        JObject jobj = token as JObject;
                        
                        string packetType = GetPacketName(jobj["Packet"]["$type"].ToString());
                    
                        if (!bag.Contains(packetType)) bag.Add(packetType);
                    }  
                });
                
                List<string> pTypes = bag.ToList();
                pTypes.Sort();
                return pTypes;
            });
        }
        private async void LoadPlayerInfo()
        {
            MatchTeams = await Task.Run(() =>
            {
                var bag = new ConcurrentBag<Player>();

                Parallel.ForEach(_jsons, str =>
                {
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.15) * 2.0))
                    };

                    if (str.Contains("S2C_CreateHero"))
                    {
                        using (StringReader sr = new StringReader(str))
                        using (JsonReader reader = new JsonTextReader(sr))
                        {
                            var token = _serializer.Deserialize(reader);
                            JObject jobj = token as JObject;

                            bag.Add(new Player(jobj["Packet"]["Name"].ToString(), jobj["Packet"]["NetID"].ToString(),
                                jobj["Packet"]["ClientID"].ToString(),
                                Boolean.Parse(jobj["Packet"]["TeamIsOrder"].ToString()),
                                jobj["Packet"]["Skin"].ToString(), jobj["Packet"]["SkinID"].ToString()));
                        }
                    }
                });

                List<Player> players = bag.ToList();
                Teams tempTeams = new Teams();
                tempTeams.Players = players;
                return tempTeams;
            });
        }
        

        private void FilterTimeLineCache()
        {
            if (TimelineFilter.Equals("All Packets"))
            {
                TimeLineSize = _tempTimelineCache.Count;
                TimelineCache = _tempTimelineCache;
            }
            else
            { 
                List<ListViewItem> temp = new List<ListViewItem>(); 
                foreach (var item in _tempTimelineCache) 
                {
                  if (TimelineFilter.Equals(item.SubItems[0].Text))
                  {
                      temp.Add(item);
                  } 
                }
                TimeLineSize = temp.Count; 
                TimelineCache = temp;
            }
        }
        
        //NOTIFY
        
        

        //PUBLISH
        public List<string[]> Publish_PacketInfo(int index)
        {
            index -= 1;
            List<string[]> data = new List<string[]>();

            using (StringReader sr  = new StringReader(_jsons[index]))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var token = _serializer.Deserialize(reader);
                JObject jobj = token as JObject;
                                                           
                foreach (KeyValuePair<string, JToken> pair in jobj["Packet"] as JObject)
                {
                    var row = new string[2];
                    row[0] = pair.Key;
                    row[1] = pair.Value.ToString();
                    data.Add(row);
                }  
            }

            return data;
        }

        public Player Publish_PlayerInfo(string username)
        {
            return _matchTeams.GetPlayerByUserName(username);
        }
        
        public string Publish_RawPacketInfo(int index)
        {
            index -= 1;
            return _jsons[index];
        }

        //Virtual-TimeLine
        public ListViewItem Publish_TimelineEntry(int itemIndex)
        {
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.

            //check to see if the requested item is currently in the cache
            if (TimelineCache != null && itemIndex >= _cacheFirstIndex && itemIndex < _cacheFirstIndex + TimelineCache.Count)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                return TimelineCache[itemIndex - _cacheFirstIndex];
            }

            //A cache miss, so create a new ListViewItem and pass it back.
            return new ListViewItem(new[] { "", "", "" });
        }
        
        public void Publish_CacheRebuild(int startIndex, int endIndex)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really necessary.
            if (TimelineCache != null && startIndex >= _cacheFirstIndex && endIndex <= _cacheFirstIndex + TimelineCache.Count)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //Now we need to rebuild the cache.
            _cacheFirstIndex = startIndex;
        }
        private string GetPacketName(string packetType)
        {
            var split = packetType.Split('.');
            var packetName = split[split.Length - 1].Split(',')[0];

            return packetName;
        }
        
        
        
        //PROPERTY-CHANGES
        private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FilePath":
                    LoadCache();
                    break;
                case "TimelineCache":
                    
                    break;
                case "TimelineFilter":
                    FilterTimeLineCache();
                    break;
                case "PacketTypes":
                    break;
                case "PacketTimeLine":
                    break;
                case "FilteredPacketTimeLine":
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