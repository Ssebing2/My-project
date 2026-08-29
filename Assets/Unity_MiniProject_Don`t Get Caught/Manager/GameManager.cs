using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _isPowerRestored;
    private bool _isGameOver;

    public void RestorePower()
    {
        _isPowerRestored = true;
    }

    public bool IsPowerRestored()
    {
        return _isPowerRestored;
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

}
