using LPRT.MVVP.Modal;

namespace LPRT.Interfaces
{
    public interface IViewCommands
    {
        /// <summary>
        /// Notifies the ViewModal that the a file directory path has been selected from the UI.
        /// </summary>
        /// <param name="path">Directory path to JSON to load.</param>
        void Notify_FileSelected(string path);
        
        /// <summary>
        /// Notifies the ViewModal that a filter has been selected.
        /// </summary>
        /// <param name="filter">Packet Type selected to filter the Packet Timeline</param>
        void Notify_FilterSelected(string filter);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        void Notify_TimelineEntrySelected(int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        string Notify_TimelineEntryNeeded(int row, int column);
    }
}