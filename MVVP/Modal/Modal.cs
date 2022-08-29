using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
        
        private List<string> _matchJson = new List<string>();
        private Teams _matchTeams = new Teams();

        private readonly TimelineCache _timeline;

         
        
        private readonly JsonSerializer _serializer;
        

        public Modal()
        {
            _serializer = new JsonSerializer();
            _timeline = new TimelineCache();
            PropertyChanged += InternalPropertyChanged;
            
            
        }


        public List<string> MatchJson
        {
            get => _matchJson;
            set
            {
                _matchJson = value;
                OnPropertyChanged(nameof(PropertyChanges.JSON_LOADED));
            }
        }
        
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(PropertyChanges.FILE_PATH));
            }
        }
        public string TimelineFilter
        {
            get => _timelineFilter;
            set
            {
                _timelineFilter = value;
                OnPropertyChanged(nameof(PropertyChanges.TIMELINE_FILTER));
            }
        }

        public string SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                _selectedPlayer = value;
                OnPropertyChanged(nameof(PropertyChanges.PLAYER_SELECTED));
            }
        }

        private List<ListViewItem> TimelineCache
        {
            get => _timeline.Cache;
            set
            {
                _timeline.Cache = value;
                OnPropertyChanged(nameof(PropertyChanges.TIMELINE_CACHE));
            }
        }

        public Teams MatchTeams
        {
            get => _matchTeams;
            set
            {
                _matchTeams = value;
                OnPropertyChanged(nameof(PropertyChanges.TEAMS));
            }
        }

        public int TimeLineSize
        {
            get => _timeline.CacheSize; 
        }


        //PARSE
        private async void LoadJson()
        {
            using (StreamReader sr = new StreamReader(FilePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                try
                {
                    MatchJson = await Task.Run(() =>
                    {
                        List<string> jsonList = new List<string>();
                        
                        while (reader.Read())
                        { 
                            if (reader.TokenType == JsonToken.StartObject) 
                            {
                                var t = _serializer.Deserialize(reader);
                                JObject jObject = (JObject)t;
                                jsonList.Add(jObject.ToString());
                            }
                        }
                        
                        return jsonList;
                    });
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
                    
                    TimelineCache = await Task.Run(() => {
                        List<ListViewItem> tempList = new List<ListViewItem>();
                        
                        while (reader.Read())
                        { 
                            if (reader.TokenType == JsonToken.StartObject) 
                            {
                                var t = _serializer.Deserialize(reader);
                                JObject jObject = (JObject)t;
                                tempList.Add(new ListViewItem(new[] { GetPacketName(jObject["Packet"]["$type"].ToString()), index.ToString(), jObject["Time"].ToString() }));
                            }
                            index++;
                        }
                        
                        return tempList;
                    });
                    _timeline.BackupCache = TimelineCache;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            
        }
        
        private async void Parse_PlayerInfo()
        {
            MatchTeams = await Task.Run(() =>
            {
                var bag = new ConcurrentBag<Player>();

                Parallel.ForEach(_matchJson, str =>
                {
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.15)))
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

        private async void Parse_NetIDInfo()
        {
            await Task.Run(() =>
            {
                var bag = new ConcurrentBag<Player>();

                Parallel.ForEach(_matchJson, str =>
                {
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.15)))
                    };

                    //TODO: Pull and store all NetIDs present within the match.            
                });
            });
        }
        

        //GETTERS
        public List<string[]> GetPacketInfo(int index)
        {
            index -= 1;
            List<string[]> data = new List<string[]>();

            using (StringReader sr  = new StringReader(_matchJson[index]))
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

        public string GetRawPacketInfo(int index)
        {
            index -= 1;
            return _matchJson[index];
        }

        //VIRTUALS
        #region VirtualCalls-Timeline
        
        private void FilterTimeLineCache()
        {
            if(TimelineCache == null) return;
            
            if (TimelineFilter.Equals("All_Packets"))
            {
                TimelineCache = _timeline.BackupCache;
            }
            else
            { 
                List<ListViewItem> temp = new List<ListViewItem>(); 
                foreach (var item in _timeline.BackupCache) 
                {
                    if (TimelineFilter.Equals(item.SubItems[0].Text))
                    {
                        temp.Add(item);
                    } 
                }
                TimelineCache = temp;
            }
        }

        public ListViewItem GetTimelineEntry(int itemIndex)
        {
            return _timeline.GetTimelineEntry(itemIndex);
        }
        
        public void RebuildTimelineCache(int startIndex, int endIndex)
        {
            _timeline.RebuildCache(startIndex,endIndex);
        }
        
        #endregion

        #region VirtualCalls-TimelineNetID

        

        #endregion
        
        //Helper-Methods
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
                case nameof(PropertyChanges.FILE_PATH):
                    LoadJson();
                    break;
                case nameof(PropertyChanges.JSON_LOADED):
                    LoadCache();
                    break;
                case nameof(PropertyChanges.TIMELINE_CACHE):
                    Parse_PlayerInfo();
                    Parse_NetIDInfo();
                    break;
                case nameof(PropertyChanges.TIMELINE_FILTER):
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