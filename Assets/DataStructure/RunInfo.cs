using System.Collections.Generic;

namespace Assets.DataStructure
{
    /// <summary>
    /// Contains the information of started coroutines.
    /// </summary>
    public class RunInfo
    {
        public int count;
        public static Dictionary<string, RunInfo> runners = new Dictionary<string, RunInfo>();
    }
}
