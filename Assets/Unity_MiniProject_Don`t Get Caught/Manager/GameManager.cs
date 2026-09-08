using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

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

    [Header("UI 사운드")]
    [SerializeField] private AudioSource _uiAudioSource;
    [SerializeField] private AudioClip _buttonHoverClip;
    [SerializeField] private AudioClip _buttonClickClip;

    [Header("인게임 BGM")]
    [SerializeField] private AudioSource _inGameBgmAudioSource;

    [Header("전력 복구 사운드")]
    [SerializeField] private AudioSource _powerAudioSource;
    [SerializeField] private AudioClip _powerRestoreClip;

    [Header("Player")]
    [SerializeField] private PlayerController _player;

    [Header("배전함 복구 UI")]
    [SerializeField] private TMP_Text _powerCountText;

    [Header("배전함 복구 표시")]
    [SerializeField] private GameObject[] _powerLights;

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

        _powerCountText.text = $"{_restoredPanelCount}          {_requiredPanelCount}";
    }

    public void RestorePower()
    {
        _restoredPanelCount++;

        _powerCountText.text = $"{_restoredPanelCount}          {_requiredPanelCount}";

        Debug.Log($"배전함 복구 수 : {_restoredPanelCount} / {_requiredPanelCount}");

        if (_restoredPanelCount <= _powerLights.Length)
        {
            _powerLights[_restoredPanelCount - 1].SetActive(true);
        }

        if (_restoredPanelCount >= _requiredPanelCount)
        {
            Debug.Log("모든 배전함 복구 완료");
            _isPowerRestored = true;

            _powerAudioSource.PlayOneShot(_powerRestoreClip);
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

        PlayerController playerController = _player.GetComponent<PlayerController>();
        playerController.StopPlayer();

        _inGameBgmAudioSource.Stop();

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

        PlayerController playerController = _player.GetComponent<PlayerController>();
        playerController.StopPlayer();

        _inGameBgmAudioSource.Stop();

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
        StartCoroutine(RestartGameDelay());
    }

    private IEnumerator RestartGameDelay()
    {
        _uiAudioSource.PlayOneShot(_buttonClickClip);

        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        StartCoroutine(GoToMainMenuDelay());
    }

    private IEnumerator GoToMainMenuDelay()
    {
        _uiAudioSource.PlayOneShot(_buttonClickClip);

        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene("MainMenu");
    }

    public void PlayButtonHoverSound()
    {
        _uiAudioSource.PlayOneShot(_buttonHoverClip);
    }

    public void PlayButtonClickSound()
    {
        _uiAudioSource.PlayOneShot(_buttonClickClip);
    }

}
