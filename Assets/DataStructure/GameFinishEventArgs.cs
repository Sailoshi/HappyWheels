using System;
using System.Collections.Generic;

namespace Assets.DataStructure
{
    /// <summary>
    /// Contains the information for the <c>SpinBlockExecuter</c> game finish event.
    /// </summary>
    public class GameFinishEventArgs : EventArgs
    {
        private List<List<BlockMaterial>> _winResult;

        public GameFinishEventArgs(List<List<BlockMaterial>> winResult)
        {
            _winResult = winResult;
        }
        public List<List<BlockMaterial>> WinResult
        {
            get
            {
                return _winResult;
            }
        }
    }
}
