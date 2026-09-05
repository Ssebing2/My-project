using UnityEngine;
using UnityEngine.UI;

public class InteractableOutline : MonoBehaviour
{
    [SerializeField] private Outline _outline;

    private void Awake()
    {
        _outline.enabled = false;
    }

    public void ShowOutline()
    {
        _outline.enabled = true;
    }

    public void HideOutline()
    {
        _outline.enabled = false;
    }
}
