using UnityEngine;

[CreateAssetMenu(fileName = "New OreGenerationChances", menuName = "OreGenerationChances")]
public class OreGenerationChances : ScriptableObject
{
    // Max chance = 100, Min chance = 0.001

    // ores in the normal world
    public float stone;
    public float tin;
    public float copper;
    public float zinc;
    public float basalt;
    public float iron;
    public float bismuth;
    public float chromite;
    public float silver;
    public float gold;
    public float platinum;
    public float hellStone; // it`s starts to be generated, but in small amounts
    public float titan;
    public float tungusten;
    public float cobald;

    //ores in hell
    public float quartz;
    public float cinnabar;
    public float palladium;
    public float obsidian;
    public float talmallit; //like stone or basalt
    public float diamond;
    public float nanite;
    public float dragonite;
    public float mythit;
}
