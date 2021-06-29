using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// it`s like an interface
public class IGrid : MonoBehaviour
{
    public const int WIDTH = 51; //x
    public const int HEIGHT = 51; //y
    public const float CELL_SIZE = 1f;
    
    public GameObject[,] gridArray = new GameObject[WIDTH,HEIGHT]; // the array includes a barrier
    public Vector2Int descentPos = new Vector2Int();

    // Get gameObject position by gridArray index
    public Vector3 GetWorldPosition(int x, int y) =>
        new Vector3(x, y) * GridBehavior.CELL_SIZE + GridBehavior.originPosition;
    
    // Get gridArray index by gameObject position
    public Vector3Int GetArrayIndex(Vector3 position) =>
        new Vector3Int(Vector3Int.FloorToInt(position).x + Mathf.Abs(Vector3Int.FloorToInt(GridBehavior.originPosition).x),
            Vector3Int.FloorToInt(position).y + Mathf.Abs(Vector3Int.FloorToInt(GridBehavior.originPosition).y), 0);
    
    // checks is Array element exists or not by world position
    public bool IsInArrayRange(Vector3 pos)
    {
        Vector3 index = GetArrayIndex(pos);
        return index.x >= 1 && index.x < GridBehavior.WIDTH - 1 && index.y >= 1 && index.y < GridBehavior.HEIGHT - 1; // -1 and 1, because array include barrier
    }
    
    // Check if tile exist on certain position or no
    public bool IsTileExists(int x, int y)
    {
        Collider2D hitInfo = Physics2D.OverlapPoint(GetWorldPosition(x, y) + new Vector3(.5f, .5f));
        return hitInfo && (!hitInfo.CompareTag("Player") && !hitInfo.CompareTag("Chest")); // TODO: maybe remake this 
    }
    
    // Checks can player get to this location or no
    public bool CanAchive(int x, int y)
    {
        if (gridArray[x, y])
            return true;
        if (gridArray[x + 1, y])
            return true;
        if (gridArray[x - 1, y])
            return true;
        if (gridArray[x, y + 1])
            return true;
        if (gridArray[x, y - 1])
            return true;

        return false;
    }
    public bool CanAchive(Vector3 pos)
    {
        Vector3Int index = GetArrayIndex(pos);
        int x = index.x;
        int y = index.y;

        if (gridArray[x, y])
            return true;
        if (gridArray[x + 1, y])
            return true;
        if (gridArray[x - 1, y])
            return true;
        if (gridArray[x, y + 1])
            return true;
        
        return gridArray[x, y - 1];
    }

    public void RemoveArrayElement(Vector3 pos)
    {
        Vector3Int index = GetArrayIndex(pos);
        
        descentPos.x = index.x;
        descentPos.y = index.y;
        
        Destroy(gridArray[index.x, index.y]);
        gridArray[index.x, index.y] = null;
    }
}
