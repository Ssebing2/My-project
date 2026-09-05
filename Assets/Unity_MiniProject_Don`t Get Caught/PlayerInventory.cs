using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int _keyCount;
    [SerializeField] private int _fuseCount;

    public void GetKey()
    {
        _keyCount++;
    }

    public bool HasKey()
    {
        return _keyCount > 0;
    }

    public void UseKey()
    {
        if (_keyCount > 0)
        {
            _keyCount--;
        }
    }

    public void GetFuse()
    {
        _fuseCount++;
    }

    public bool HasFuse()
    {
        return _fuseCount > 0;
    }

    public void UseFuse()
    {
        if (_fuseCount > 0)
        {
            _fuseCount--;
        }
    }
}
