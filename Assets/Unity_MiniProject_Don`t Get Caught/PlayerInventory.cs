using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private bool _hasKey;

    public void GetKey()
    {
        _hasKey = true;
    }

    public bool HasKey()
    {
        return _hasKey;
    }
}
