[System.Serializable]
public class ExtractedOresData
{
    public int[] ores = new int[24];

    public ExtractedOresData(int[] ores) =>
        this.ores = ores;
}
