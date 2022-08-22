using System.Diagnostics;
using LPRT.MVVP.Modal;

namespace LPRT.MVVP.View
{
    public class Player
    {
        private string _username;
        private string _netID;
        private string _clientID;
        
        private string _team;
        
        private string _champion;
        private string _skinID;

        public Player(string username, string netID, string clientID, bool team, string champion, string skinID)
        {
            _username = username;
            _netID = netID;
            _clientID = clientID;
            _team = CheckTeam(team);
            _champion = champion;
            _skinID = skinID;
        }

        public string Username
        {
            get => _username;
            set => _username = value;
        }

        public string NetId
        {
            get => _netID;
            set => _netID = value;
        }

        public string ClientId
        {
            get => _clientID;
            set => _clientID = value;
        }

        public string Team
        {
            get => _team;
            set => _team = value;
        }

        public string Champion
        {
            get => _champion;
            set => _champion = value;
        }

        public string SkinId
        {
            get => _skinID;
            set => _skinID = value;
        }

        private string CheckTeam(bool b)
        {
            if (b) return "Order";
            return "Chaos";
        }
    }
}