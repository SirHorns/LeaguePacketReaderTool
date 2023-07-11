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

        private readonly PacketTimelineCache _packetTimeline;
        
        private readonly JsonSerializer _serializer;
        
        public Modal()
        {
            _serializer = new JsonSerializer();
            _packetTimeline = new PacketTimelineCache();
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
            get => _packetTimeline.Cache;
            set
            {
                _packetTimeline.Cache = value;
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
            get => _packetTimeline.CacheSize; 
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
                    _packetTimeline.BackupCache = TimelineCache;
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