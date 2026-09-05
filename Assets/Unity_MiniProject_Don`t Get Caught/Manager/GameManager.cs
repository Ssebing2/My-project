using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("게임오버엔딩")]
    [SerializeField] private GameObject _gameOverRawImage;
    [SerializeField] private VideoPlayer _gameOverVideoPlayer;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _mainMenuButton;
    [SerializeField] private GameObject _gameOverPanel;


    [Header("게임클리어엔딩")]
    [SerializeField] private GameObject _gameClearRawImage;
    [SerializeField] private VideoPlayer _gameClearVideoPlayer;
    [SerializeField] private GameObject _gameClearMainMenuButton;
    [SerializeField] private GameObject _gameClearPanel;

    [Header("Enemy")]
    [SerializeField] private EnemyPatrol _enemy;

    [Header("복구 배전함 수")]
    [SerializeField] private int _requiredPanelCount = 3;

    private int _restoredPanelCount; // 현재 배전함 복구 수
    private bool _isPowerRestored;
    private bool _isGameOver;
    private bool _isGameClear;

    private void Start()
    {
        _gameOverPanel.SetActive(false);
        _gameClearPanel.SetActive(false);

        _gameOverVideoPlayer.loopPointReached += OnGameOverVideoFinished;
        _gameClearVideoPlayer.loopPointReached += OnGameClearVideoFinished;
    }

    public void RestorePower()
    {
        _restoredPanelCount++;

        Debug.Log($"배전함 복구 수 : {_restoredPanelCount} / {_requiredPanelCount}");

        if (_restoredPanelCount >= _requiredPanelCount)
        {
            Debug.Log("모든 배전함 복구 완료");
            _isPowerRestored = true;
        }
    }

    public bool IsPowerRestored()
    {
        return _isPowerRestored;
    }

    public void GameOver()
    {
        if (_isGameClear || _isGameOver)
        {
            return;
        }

        _enemy.StopEnemy();

        _isGameOver = true;

        _gameClearPanel.SetActive(false);
        _gameOverPanel.SetActive(true);
        _gameOverRawImage.SetActive(true);

        _gameOverVideoPlayer.Play();
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    public void GameClear()
    {
        if (_isGameClear || _isGameOver)
        {
            return;
        }

        _enemy.StopEnemy();

        _isGameClear = true;

        _gameOverPanel.SetActive(false);
        _gameClearPanel.SetActive(true);
        _gameClearRawImage.SetActive(true);

        Debug.Log("GameClear 영상 Play 실행!");

        _gameClearVideoPlayer.Play();
    }

    public bool IsGameClear()
    {
        return _isGameClear;
    }

    private void OnGameOverVideoFinished(VideoPlayer source)
    {
        Debug.Log("게임오버 영상 재생 끝!");

        _restartButton.SetActive(true);
        _mainMenuButton.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnGameClearVideoFinished(VideoPlayer source)
    {
        Debug.Log("GameClear 영상 종료 이벤트 들어옴!");

        _gameClearMainMenuButton.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
