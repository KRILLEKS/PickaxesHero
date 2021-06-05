[System.Serializable]
public class GridData
{
    public bool[,] gridArray;
    public int[] descentPos = new int[2];

    public GridData(GridBehavior gridBehavior)
    {
        gridArray = new bool[GridBehavior.WIDTH, GridBehavior.HEIGHT];

        for (int x = 0; x < GridBehavior.WIDTH; x++)
            for (int y = 0; y < GridBehavior.HEIGHT; y++)
                if (gridBehavior.gridArray[x,y])
                    gridArray[x, y] = true;

        descentPos[0] = gridBehavior.descentPos.x;
        descentPos[1] = gridBehavior.descentPos.y;
    }
}
