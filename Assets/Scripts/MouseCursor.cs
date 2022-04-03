using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorTextrue;
    public void onHover()
    {
        Cursor.SetCursor(cursorTextrue, Vector2.zero, CursorMode.Auto);
    }
    public void onExitHover()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
