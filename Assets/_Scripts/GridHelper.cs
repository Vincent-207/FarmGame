using UnityEngine;
using UnityEngine.InputSystem;

public class GridHelper
{
    public static Vector2Int GetCurrentMouseGridPos()
    {
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2Int gridPos = Vector2Int.RoundToInt(worldMousePosition);
        return gridPos;
    }
}
