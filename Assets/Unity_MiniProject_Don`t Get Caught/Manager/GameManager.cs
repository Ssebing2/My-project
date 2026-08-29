using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _isPowerRestored;

    public void RestorePower()
    {
        _isPowerRestored = true;
    }

    public bool IsPowerRestored()
    {
        return _isPowerRestored;
    }
}
