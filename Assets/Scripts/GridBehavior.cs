using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : IGrid
{
    [SerializeField] private bool createWhiteLines = true;
    [SerializeField] private GameObject gridPrefab;

    // local variables
    private int _startX;
    private int _startY;
    private int _endX = 17;
    private int _endY = 15;

    // path 
    public List<GameObject> g_path;

    // constants
    public const int WIDTH = 51; //x
    public const int HEIGHT = 51; //y
    public const float CELL_SIZE = 1f;
    public static readonly Vector3 originPosition = new Vector3(-25, -25);

    private void Awake()
    {
        _startX = Mathf.Abs(Vector3Int.FloorToInt(GetWorldPosition(0, 0)).x);
        _startY = Mathf.Abs(Vector3Int.FloorToInt(GetWorldPosition(0, 0)).y);

        gridArray = new GameObject[WIDTH, HEIGHT];

        CreateWhiteLines();
    }


#region Grid

    public void CreateWhiteLines()
    {
        if (!createWhiteLines)
            return;

        for (int x = 0; x <= WIDTH; x++)
            Debug.DrawLine(GetWorldPosition(x, 0),
                GetWorldPosition(x, HEIGHT),
                Color.white,
                100f);

        for (int y = 0; y <= HEIGHT; y++)
            Debug.DrawLine(GetWorldPosition(0, y),
                GetWorldPosition(WIDTH, y),
                Color.white,
                100f);
    }

    public void GenerateGrid()
    {
        for (int x = 0; x < WIDTH; x++)
            for (int y = 0; y < HEIGHT; y++)
                if (!IsTileExists(x, y))
                    InstantiateGridPrefab(x, y);
    }

    public void LoadGrid(GridData gridData)
    {
        ResetGrid();

        for (int x = 0; x < WIDTH; x++)
            for (int y = 0; y < HEIGHT; y++)
                if (gridData.gridArray[x, y])
                    InstantiateGridPrefab(x, y);
    }

    public void ResetGrid()
    {
        if (gridArray != null)
            foreach (var gridPrefab in gridArray)
                Destroy(gridPrefab);

        gridArray = new GameObject[WIDTH, HEIGHT];
    }

#endregion


#region instantiate GridPrefab

    private void InstantiateGridPrefab(int x, int y)
    {
        GameObject obj = Instantiate(gridPrefab,
            GetWorldPosition(x, y) +
            new Vector3(CELL_SIZE, CELL_SIZE) * .5f +
            new Vector3(0, 0, -10),
            Quaternion.identity);
        obj.GetComponent<GridStat>().x = x;
        obj.GetComponent<GridStat>().y = y;
        gridArray[x, y] = obj;
        obj.transform.SetParent(gameObject.transform);
    }

    // Instantiate gridPrefab when block breaks 
    public void InstantiateNewGridPrefab(Vector3 pos)
    {
        Vector3Int index = GetArrayIndex(pos);
        int x = index.x;
        int y = index.y;

        InstantiateGridPrefab(x, y);
    }

#endregion


#region Sets Path

    // Sets EndX,EndY,StartX,startY
    public void SetPosition(Vector3 position, bool start)
    {
        Vector3Int index = GetArrayIndex(position);

        if (start) // true for start position
            SetStartPosition();

        else // false for end position
            SetEndPosition();

        void SetEndPosition()
        {
            _endX = index.x;
            _endY = index.y;
        }

        void SetStartPosition()
        {
            _startX = index.x;
            _startY = index.y;
        }
    }

    // SetDistance from player to each tile
    public void SetDistance()
    {
        InitialSetUp();

        // step < Vector3.Distance(new Vector3(startX,startY), new Vector3(endX,endY)) * 4.5 calculate maximum possible number of steps that need to reach desired location
        for (int step = 1;
            step < HEIGHT * WIDTH &&
            step <
            Vector3.Distance(new Vector3(_startX, _startY),
                new Vector3(_endX, _endY)) *
            4.5;
            step++)
            foreach (GameObject obj in gridArray)
                if (obj && obj.GetComponent<GridStat>().stepsToGo == step - 1)
                    TestFourDirections(obj.GetComponent<GridStat>().x,
                        obj.GetComponent<GridStat>().y,
                        step);

        // Reset our grid settings
        void InitialSetUp()
        {
            foreach (var item in gridArray)
                if (item)
                    item.GetComponent<GridStat>().stepsToGo = -1;

            gridArray[_startX, _startY].GetComponent<GridStat>().stepsToGo = 0;
        }
    }

    // Set the visited value to a step in 4 directions
    private void TestFourDirections(int x, int y, int step)
    {
        if (TestDirection(x, y, -1, 1)) // up
            SetVisited(x, y + 1, step);
        if (TestDirection(x, y, -1, 2)) // down
            SetVisited(x, y - 1, step);
        if (TestDirection(x, y, -1, 3)) // left
            SetVisited(x - 1, y, step);
        if (TestDirection(x, y, -1, 4)) // right
            SetVisited(x + 1, y, step);

        // Sets visited to the same value as the current step
        void SetVisited(int _x, int _y, int _step)
        {
            if (gridArray[_x, _y])
                gridArray[_x, _y].GetComponent<GridStat>().stepsToGo = _step;
        }
    }

    // Tests where we can go or where we can't in 1 direction
    private bool TestDirection(int x, int y, int step, int direction)
    {
        //int direction tells whish case to use
        //1 is up, 2 is down, 3 is left, 4 is right
        switch (direction)
        {
            case 1: //up
                return y + 1 < HEIGHT &&
                       gridArray[x, y + 1] &&
                       gridArray[x, y + 1].GetComponent<GridStat>().stepsToGo ==
                       step;

            case 2: //down
                return y - 1 > -1 &&
                       gridArray[x, y - 1] &&
                       gridArray[x, y - 1].GetComponent<GridStat>().stepsToGo ==
                       step;

            case 3: //left
                return x - 1 > -1 &&
                       gridArray[x - 1, y] &&
                       gridArray[x - 1, y].GetComponent<GridStat>().stepsToGo ==
                       step;

            case 4: //right
                return x + 1 < WIDTH &&
                       gridArray[x + 1, y] &&
                       gridArray[x + 1, y].GetComponent<GridStat>().stepsToGo ==
                       step;

            default:
                return false;
        }
    }

    // Sets path as g_Path var
    public void SetPath()
    {
        g_path.Clear();

        int step = -1;
        // X and y can be changed for logic inside program, endX and endY - can`t,
        // So u can use them to check position regardless of changes x and y
        int x = _endX;
        int y = _endY;

        AddEndPoint();
        AddOtherPoints();

        g_path.Reverse();

        //sets all the path, except the endpoints
        void AddOtherPoints()
        {
            for (; step > -1; step--)
            {
                if (TestDirection(x, y, step, 1)) // up
                    g_path.Add(gridArray[x, ++y]);
                else if (TestDirection(x, y, step, 2)) // down
                    g_path.Add(gridArray[x, --y]);
                else if (TestDirection(x, y, step, 3)) // left
                    g_path.Add(gridArray[--x, y]);
                else if (TestDirection(x, y, step, 4)) // right
                    g_path.Add(gridArray[++x, y]);
            }
        }

        // Adds an endpoint to the path and sets step
        void AddEndPoint()
        {
            if (gridArray[x, y] &&
                gridArray[x, y].GetComponent<GridStat>().stepsToGo > 0 &&
                !IsTileExists(x, y))
            {
                g_path.Add(gridArray[x, y]);
                step = gridArray[x, y].GetComponent<GridStat>().stepsToGo - 1;
            }
            // Calls if we can`t reach endpoint, but can probably reach ore that needs to be mined
            else if (WIDTH - 1 != x &&
                     x != 0 &&
                     HEIGHT - 1 != y &&
                     y != 0 &&
                     IsTileExists(x, y) &&
                     CanAchive(x, y))
            {
                PathToExtractedBlock();
            }

            // Calculate path to the block to be extracted or writes that it is impossible to achive desired location
            void PathToExtractedBlock()
            {
                // up, so block at the bottom
                if (gridArray[_endX, _endY + 1] &&
                    gridArray[_endX, _endY + 1].GetComponent<GridStat>()
                                               .stepsToGo !=
                    -1
                ) // checks can achive or not
                {
                    step = gridArray[_endX, _endY + 1].GetComponent<GridStat>()
                        .stepsToGo;
                    x = _endX;
                    y = _endY + 1;
                }

                // down, so block at the top
                if (gridArray[_endX, _endY - 1] &&
                    (step == -1 ||
                     gridArray[_endX, _endY - 1].GetComponent<GridStat>()
                                                .stepsToGo <
                     step) &&
                    gridArray[_endX, _endY - 1].GetComponent<GridStat>()
                                               .stepsToGo !=
                    -1) // checks can achive or not
                {
                    step = gridArray[_endX, _endY - 1].GetComponent<GridStat>()
                        .stepsToGo;
                    x = _endX;
                    y = _endY - 1;
                }

                // left, so block on the right
                if (gridArray[_endX - 1, _endY] &&
                    (step == -1 ||
                     gridArray[_endX - 1, _endY].GetComponent<GridStat>()
                                                .stepsToGo <
                     step) &&
                    gridArray[_endX - 1, _endY].GetComponent<GridStat>()
                                               .stepsToGo !=
                    -1) // checks can achieve or not
                {
                    step = gridArray[_endX - 1, _endY].GetComponent<GridStat>()
                        .stepsToGo;
                    y = _endY;
                    x = _endX - 1;
                }

                //right, so block on the left
                if (gridArray[_endX + 1, _endY] &&
                    (step == -1 ||
                     gridArray[_endX + 1, _endY].GetComponent<GridStat>()
                                                .stepsToGo <
                     step) &&
                    gridArray[_endX + 1, _endY].GetComponent<GridStat>()
                                               .stepsToGo !=
                    -1) // checks can achieve or not
                {
                    step = gridArray[_endX + 1, _endY].GetComponent<GridStat>()
                        .stepsToGo;
                    y = _endY;
                    x = _endX + 1;
                }

                if (step > -1)
                {
                    g_path.Add(gridArray[x, y]);
                    step = gridArray[x, y].GetComponent<GridStat>().stepsToGo -
                           1;
                }

                else
                {
                    Debug.Log("Can`t reach desired location");
                    return;
                }
            }
        }
    }

#endregion
}