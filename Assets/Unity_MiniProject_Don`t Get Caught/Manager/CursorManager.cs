using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("Ä¿¼­")]
    [SerializeField] private Texture2D _defaultCursor;
    [SerializeField] private Texture2D _hoverCursor;
    [SerializeField] private Texture2D _clickCursor;

    private Vector2 _hotspot = Vector2.zero;

    private void Start()
    {
        SetDefaultCursor();
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(_defaultCursor, _hotspot, CursorMode.Auto);
    }

    public void SetHoverCursor()
    {
        Cursor.SetCursor(_hoverCursor, _hotspot, CursorMode.Auto);
    }

    public void SetClickCursor()
    {
        Cursor.SetCursor(_clickCursor, _hotspot, CursorMode.Auto);
    }
}
