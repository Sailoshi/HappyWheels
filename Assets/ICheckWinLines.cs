using Assets.DataStructure;
using System.Collections.Generic;

namespace Assets
{
    public interface ICheckWinLines
    {
        WinLineData CalculateWinLine(List<BlockMaterial> line);
    }
}
