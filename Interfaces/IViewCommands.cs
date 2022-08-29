using System.Windows.Forms;
using LPRT.MVVP.Modal;

namespace LPRT.Interfaces
{
    public interface IViewCommands
    {
        //Notify
        void SelectedFile(string path);
        void SelectedTimelineFilter(string filter);
        void SelectedTimelineEntry(int index);
        void SelectedPlayer(string username);
        void SelectedNetID(string netID);
        void SelectSentRecieve(string status);
        
        //Request
        ListViewItem Request_TimelineEntry(int itemIndex);
        void Request_RebuildCache(int startIndex, int endIndex);
    }
}