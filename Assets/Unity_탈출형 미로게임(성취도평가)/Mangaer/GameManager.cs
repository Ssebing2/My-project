using UnityEngine;
using UnityEngine.SceneManagement;

public enum EGameState
{
    Ready,
    Playing,
    Clear,
    GameOver
}

public class GameManager2 : MonoBehaviour
{
    [Header("Game Setting")]
    [SerializeField] private float _maxTime = 60.0f;

    private float _currentTime;
    private EGameState _gameState;

    public float CurrentTime => _currentTime;
    public EGameState GameState => _gameState;

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (_gameState == EGameState.Playing)
        {
            UpdateTimer();
        }
    }

    private void InitializeGame()
    {
        _currentTime = _maxTime;
        _gameState = EGameState.Ready;

        Debug.Log("READY");
    }

    public void StartGame()
    {
        _gameState = EGameState.Playing;

        Debug.Log("GAME START");
    }

    public void ClearGame()
    {
        if (_gameState != EGameState.Playing)
        {
            return;
        }

        _gameState = EGameState.Clear;

        Debug.Log("GAME CLEAR");
    }

    public void GameOver()
    {
        if (_gameState != EGameState.Playing)
        {
            return;
        }

        _gameState = EGameState.GameOver;

        Debug.Log("GAME OVER");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateTimer()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0.0f)
        {
            _currentTime = 0.0f;

            GameOver();
        }
    }
}
