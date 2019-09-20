using System.Collections.Generic;

namespace Assets.DataStructure
{
    /// <summary>
    /// Contains the information for the possible win line.
    /// </summary>
    public class WinLineData
    {
        private int _credits = 0;
        private List<BlockMaterial> _winLine = new List<BlockMaterial>();

        public WinLineData(List<BlockMaterial> winline)
        {
            _winLine = winline;
        }

        public List<BlockMaterial> WinLine
        {
            get
            {
                return _winLine;
            }
        }

        /// <summary>
        /// The material information for this line.
        /// </summary>
        public BlockMaterial BlockMaterialData { get; set; }
        /// <summary>
        /// The won credits for this line.
        /// </summary>
        public int Credits { get; set; }

        /// <summary>
        /// Marks the line as a win.
        /// </summary>
        public bool IsWin { get; set; }
    }
}
