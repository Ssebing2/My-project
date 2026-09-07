using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;



public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private UnityEngine.UI.Image _hoverDarkOverlay;
    [SerializeField] private VideoPlayer _pauseMenuVideoPlayer;

    [Header("버튼 호버 연출")]
    [SerializeField] private Image _settingButtonImage;
    [SerializeField] private Image _mainMenuButtonImage;

    [Header("인게임 오디오소스")]
    [SerializeField] private AudioSource _inGameBgmAudioSource;
    [SerializeField] private bool _isBgmOn = true;

    [Header("UI 사운드")]
    [SerializeField] private AudioSource _uiAudioSource;
    [SerializeField] private AudioClip _buttonHoverClip;
    [SerializeField] private AudioClip _buttonClickClip;

    private bool _isPaused;

    private void Start()
    {
        _pausePanel.SetActive(false);
        _settingsPanel.SetActive(false);
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                ResumeGame();
            }

            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        _isPaused = true;

        _pausePanel.SetActive(true);

        _pauseMenuVideoPlayer.Play();

        _inGameBgmAudioSource.Pause();

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        _isPaused = false;

        _pauseMenuVideoPlayer.Stop();

        _pausePanel.SetActive(false);
        _settingsPanel.SetActive(false);

        if (_isBgmOn)
        {
            _inGameBgmAudioSource.UnPause();
        }
        else
        {
            _inGameBgmAudioSource.Pause();
        }

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenSettings()
    {
        _uiAudioSource.PlayOneShot(_buttonClickClip);

        _pausePanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        _uiAudioSource.PlayOneShot(_buttonClickClip);

        _settingsPanel.SetActive(false);
        _pausePanel.SetActive(true);
    }

    public void BGMOn()
    {
        _isBgmOn = true;
        _inGameBgmAudioSource.UnPause();
    }

    public void BGMOff()
    {
        _isBgmOn = false;
        _inGameBgmAudioSource.Pause();
    }

    public void PlayButtonHoverSound()
    {
        Debug.Log($"호버 사운드 실행! Frame : {Time.frameCount}");
        _uiAudioSource.PlayOneShot(_buttonHoverClip);
    }

    public void GoToMainMenu()
    {
        StartCoroutine(GoToMainMenuDelay());
    }

    private IEnumerator GoToMainMenuDelay()
    {
        _uiAudioSource.PlayOneShot(_buttonClickClip);

        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonHoverEnter()
    {
        Color color = _hoverDarkOverlay.color;
        color.a = 0.25f;
        _hoverDarkOverlay.color = color;

        Debug.Log($"어둡게 만든 오브젝트 : {_hoverDarkOverlay.gameObject.name}");
        Debug.Log($"현재 Alpha : {_hoverDarkOverlay.color.a}");
    }

    public void ButtonHoverExit()
    {
        Color color = _hoverDarkOverlay.color;
        color.a = 0f;
        _hoverDarkOverlay.color = color;

        Debug.Log("Hover Exit!");
    }

    public void SettingHoverEnter()
    {
        // MAIN MENU만 살짝 어둡게
        Color color = _mainMenuButtonImage.color;
        color.a = 0.35f;
        _mainMenuButtonImage.color = color;
    }

    public void SettingHoverExit()
    {
        Color color = _mainMenuButtonImage.color;
        color.a = 35f;
        _mainMenuButtonImage.color = color;
    }

    public void MainMenuHoverEnter()
    {
        // SETTINGS만 살짝 어둡게
        Color color = _settingButtonImage.color;
        color.a = 0.35f;
        _settingButtonImage.color = color;
    }

    public void MainMenuHoverExit()
    {
        Color color = _settingButtonImage.color;
        color.a = 35f;
        _settingButtonImage.color = color;
    }
}
