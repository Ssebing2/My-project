using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private bool _hasKey;
    [SerializeField] private bool _hasFuse;

    public void GetKey()
    {
        _hasKey = true;
    }

    public bool HasKey()
    {
        return _hasKey;
    }

    public void UseKey()
    {
        _hasKey = false;
    }

    public void GetFuse()
    {
        _hasFuse = true;
    }

    public bool HasFuse()
    {
        return _hasFuse;
    }

    public void UseFuse()
    {
        _hasFuse = false;
    }
}
