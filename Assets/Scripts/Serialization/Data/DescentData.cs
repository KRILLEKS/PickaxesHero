[System.Serializable]
public class DescentData
{
    public bool descentWasSpawned;
    public float[] position = new float[3];

    public DescentData(DescentToTheNextLevel descentToTheNextLevel)
    {
        descentWasSpawned = descentToTheNextLevel.descentWasSpawned;

        if (descentWasSpawned)
        {
            position[0] = descentToTheNextLevel.descent.transform.position.x;
            position[1] = descentToTheNextLevel.descent.transform.position.y;
            position[2] = descentToTheNextLevel.descent.transform.position.z;
        }
    }
}
