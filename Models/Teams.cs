using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LPRT.View;

namespace LPRT
{
    public class Teams: INotifyPropertyChanged
    {
        private List<Player> _players = new();
        private List<Player> _teamOrder = new();
        private List<Player> _teamChaos = new();


        public Teams(List<Player> players)
        {
            _players = players;
            
            foreach (var player in _players)
            {
                if (Boolean.Parse(player.Team))
                {
                    _teamOrder.Add(player);
                }
                else
                {
                    _teamChaos.Add(player);
                }
            }
        }

        public Teams()
        {
        }

        public Player AddPlayer(string username, string netId, string clientId, bool team, string champion, string skinId, bool isBot = false)
        {
            Player newPlayer = new Player(username, netId, clientId, team, champion, skinId, isBot);
            
            _players.Add(newPlayer);
            
            if (team)
            {
                _teamOrder.Add(newPlayer);
            }
            else
            {
                _teamChaos.Add(newPlayer);
            }

            return newPlayer;
        }
        public Player AddPlayer(Player player)
        {
            _players.Add(player);
            
            if (Boolean.Parse(player.Team))
            {
                _teamOrder.Add(player);
            }
            else
            {
                _teamChaos.Add(player);
            }

            return player;
        }

        //Team
        public List<Player> Players
        {
            get => _players;
            set
            {
                _players = value;
                OnPropertyChanged(nameof(PropertyChanges.PLAYERS_UPDATED));
            }
        }

        public List<Player> TeamOrder
        {
            get => _teamOrder;
            set
            {
                _teamOrder = value;
                OnPropertyChanged(nameof(PropertyChanges.PLAYERS_UPDATED));
            }
        }

        public List<Player> TeamChaos
        {
            get => _teamChaos;
            set
            {
                _teamChaos = value;
                OnPropertyChanged(nameof(PropertyChanges.PLAYERS_UPDATED));
            }
        }
        
        //Player
        public Player GetPlayerByUserName(string username)
        {
            Player player = null;
            foreach (var plyr in _players)
            {
                if (plyr.Username.Equals(username))
                {
                    player = plyr;
                }
            }

            return player;
        }
        
        public Player GetPlayerByNetID(string netID)
        {
            Player player = null;
            foreach (var plyr in _players)
            {
                if (plyr.NetId.Equals(netID))
                {
                    player = plyr;
                }
            }

            return player;
        }
        
        public Player GetPlayerByClientID(string clientID)
        {
            Player player = null;
            foreach (var plyr in _players)
            {
                if (plyr.ClientId.Equals(clientID))
                {
                    player = plyr;
                }
            }

            return player;
        }
        
        public Player GetPlayerByChampion(string champion)
        {
            Player player = null;
            foreach (var plyr in _players)
            {
                if (plyr.Champion.Equals(champion))
                {
                    player = plyr;
                }
            }

            return player;
        }
        
        //PROPERTY-CHANGES
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}