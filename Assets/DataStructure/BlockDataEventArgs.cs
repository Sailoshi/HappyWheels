using System;

namespace Assets.DataStructure
{
    /// <summary>
    /// Contains the information for the <c>SpinBlock</c> spin finish event.
    /// </summary>
    public class BlockDataEventArgs : EventArgs
    {
        private BlockMaterial _material;
        private int _groupIndex;
        private int _blockIndex;

        public BlockDataEventArgs(BlockMaterial material, int groupIndex, int blockIndex)
        {
            _material = material;
            _groupIndex = groupIndex;
            _blockIndex = blockIndex;
        }
        public BlockMaterial Material
            {
            get
            {
                return _material;
            }
        }
        public int GroupIndex
        {
            get
            {
                return _groupIndex;
            }
        }
        public int BlockIndex {
            get
            {
                return _blockIndex;
            }
        }
    }
}
