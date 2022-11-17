using System;
using System.Collections.Generic;
using LPRT.MVVP.View;

namespace LPRT.MVVP.Modal
{
    public class Teams
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

        public Player AddPlayer(string username, string netID, string clientID, bool team, string champion, string skinID)
        {
            Player newPlayer = new Player(username, netID, clientID, team, champion, skinID);
            
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
            set => _players = value;
        }

        public List<Player> TeamOrder
        {
            get => _teamOrder;
            set => _teamOrder = value;
        }

        public List<Player> TeamChaos
        {
            get => _teamChaos;
            set => _teamChaos = value;
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
    }
}