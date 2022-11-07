using System.Collections.Generic;
using System.Windows.Forms;

namespace LPRT.MVVP.Modal
{
    public  abstract class Cache
    {
        /// <summary>
        /// Cache that stores the current filtered timeline. 
        /// </summary>
        private List<ListViewItem> _activeCache;
        /// <summary>
        /// Stores the index of the first item in the cache
        /// </summary>
        private int _firstIndex;
        /// <summary>
        /// Stores the currently selected filter for which packets should be displayed.
        /// </summary>
        private string _timelineFilter;
        /// <summary>
        /// 
        /// </summary>
        private int _cacheSize;
        
        /// <summary>
        /// 
        /// </summary>
        public List<ListViewItem> MainCache
        {
            get => _activeCache;
            set
            {
                _cacheSize = value.Count;
                _activeCache = value;
            }
        }
        /// <summary>
        /// Stores the untampered timeline cached used for creating a new filtered timelineCache
        /// </summary>
        public List<ListViewItem> BackupCache { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CacheSize => _cacheSize;
        
        //Virtual Methods
        public ListViewItem GetTimelineEntry(int itemIndex)
        {
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.

            //check to see if the requested item is currently in the cache
            if (MainCache != null && itemIndex >= _firstIndex && itemIndex < _firstIndex + MainCache.Count)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                return MainCache[itemIndex - _firstIndex];
            }

            //A cache miss, so create a new ListViewItem and pass it back.
            return new ListViewItem(new[] { "", "", "" });
        }
        
        public void RebuildCache(int startIndex, int endIndex)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really necessary.
            if (MainCache != null && startIndex >= _firstIndex && endIndex <= _firstIndex + MainCache.Count)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //Now we need to rebuild the cache.
            _firstIndex = startIndex;
        }
    }
}