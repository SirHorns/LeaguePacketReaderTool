using System.Windows.Forms;
using LPRT.MVVP.Modal;

namespace LPRT.Interfaces
{
    public interface IViewCommands
    {
        //Notify
        void Notify_FileSelected(string path);
        void Notify_FilterSelected(string filter);
        void Notify_TimelineEntrySelected(int index);
        //Request
        ListViewItem Request_TimelineEntry(int itemIndex);
        void Request_RebuildCache(int startIndex, int endIndex);
    }
}