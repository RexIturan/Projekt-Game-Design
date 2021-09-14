using UnityEngine;

public interface IMapDrawer
{
    public void DrawGrid();
    public void clearCursor();
    public void DrawBoxCursorAt(Vector3 start, Vector3 end);
    public void DrawCursorAt(Vector3 pos);
}
