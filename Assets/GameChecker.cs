using System.Collections.Generic;
using Assets.DataStructure;

namespace Assets
{
    public class GameChecker : ICheckWinLines
    {
        public WinLineData CalculateWinLine(List<BlockMaterial> line)
        {
            WinLineData result = new WinLineData(line);

            BlockMaterial lastBlock = null;
            var isWin = true;

            foreach (var block in line)
            {
                if (lastBlock != null && block.MaterialIdentifier != lastBlock.MaterialIdentifier)
                {
                    isWin = false;
                    break;
                }
                lastBlock = block;
                result.BlockMaterialData = block;
            }
            
            result.IsWin = isWin;            

            return result;
        }
    }
}
