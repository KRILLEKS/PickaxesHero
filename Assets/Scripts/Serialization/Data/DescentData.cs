[System.Serializable]
public class DescentData
{
    public bool descentWasSpawned;
    public float[] position = new float[3];

    public DescentData(NextLevelLoadController nextLevelLoadController)
    {
        descentWasSpawned = nextLevelLoadController.descentWasSpawned;

        if (descentWasSpawned)
        {
            position[0] = nextLevelLoadController.descent.transform.position.x;
            position[1] = nextLevelLoadController.descent.transform.position.y;
            position[2] = nextLevelLoadController.descent.transform.position.z;
        }
    }
}
