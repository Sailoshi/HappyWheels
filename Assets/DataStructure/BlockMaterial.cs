namespace Assets.DataStructure
{
    /// <summary>
    /// Contains the information of the texture.
    /// </summary>
    public class BlockMaterial
    {
        public string MaterialName { get; set; }
        public int MaterialIdentifier { get; set; }
        public BlockMaterial(string materialName, int materialIdentifier)
        {
            MaterialName = materialName;
            MaterialIdentifier = materialIdentifier;
        }
    }
}