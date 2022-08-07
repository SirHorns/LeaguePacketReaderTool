namespace LPRT.Interfaces
{
    public interface IViewFunctions
    {
        /// <summary>
        /// Called by View.
        /// Tells the Modal to load the json file from the path to parse and store match packet information.
        /// </summary>
        /// <param name="path">Directory path to JSON to load.</param>
        void LoadPacketFile(string path);
        /// <summary>
        /// 
        /// </summary>
        void LoadPacketTimeLine();
        /// <summary>
        /// 
        /// </summary>
        void LoadPacketTimeLineFilters();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        void FilterPacketTimeLine(string filter);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        void LoadPacketInfo(int index);
    }
}