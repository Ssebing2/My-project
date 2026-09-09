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

    [Header("¹öÆ° È£¹ö ¿¬Ãâ")]
    [SerializeField] private Image _settingButtonImage;
    [SerializeField] private Image _mainMenuButtonImage;
    [SerializeField] private Image _howToPlayButtonImage;

    [Header("ÀÎ°ÔÀÓ ¿Àµð¿À¼Ò½º")]
    [SerializeField] private AudioSource _inGameBgmAudioSource;
    [SerializeField] private bool _isBgmOn = true;

    [Header("UI »ç¿îµå")]
    [SerializeField] private AudioSource _uiAudioSource;
    [SerializeField] private AudioClip _buttonHoverClip;
    [SerializeField] private AudioClip _buttonClickClip;

    [Header("HOW TO PLAY")]
    [SerializeField] private GameObject _howToPlayPanel;

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

    public void OpenHowToPlay()
    {
        _uiAudioSource.PlayOneShot(_buttonClickClip);

        _pausePanel.SetActive(false);
        _howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        _uiAudioSource.PlayOneShot(_buttonClickClip);

        _howToPlayPanel.SetActive(false);
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
        Debug.Log($"È£¹ö »ç¿îµå ½ÇÇà! Frame : {Time.frameCount}");
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

        Debug.Log($"¾îµÓ°Ô ¸¸µç ¿ÀºêÁ§Æ® : {_hoverDarkOverlay.gameObject.name}");
        Debug.Log($"ÇöÀç Alpha : {_hoverDarkOverlay.color.a}");
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
        // MAIN MENU »ìÂ¦ ¾îµÓ°Ô
        Color color = _mainMenuButtonImage.color;
        color.a = 0.35f;
        _mainMenuButtonImage.color = color;

        // HOW TO PLAY »ìÂ¦ ¾îµÓ°Ô
        Color color2 = _howToPlayButtonImage.color;
        color2.a = 0.35f;
        _howToPlayButtonImage.color = color2;
    }

    public void SettingHoverExit()
    {
        Color color = _mainMenuButtonImage.color;
        color.a = 35f;
        _mainMenuButtonImage.color = color;

        Color color2 = _howToPlayButtonImage.color;
        color2.a = 35f;
        _howToPlayButtonImage.color = color2;
    }

    public void MainMenuHoverEnter()
    {
        // SETTINGS¸¸ »ìÂ¦ ¾îµÓ°Ô
        Color color = _settingButtonImage.color;
        color.a = 0.35f;
        _settingButtonImage.color = color;

        // HOW TO PLAY »ìÂ¦ ¾îµÓ°Ô
        Color color2 = _howToPlayButtonImage.color;
        color2.a = 0.35f;
        _howToPlayButtonImage.color = color2;
    }

    public void MainMenuHoverExit()
    {
        Color color = _settingButtonImage.color;
        color.a = 35f;
        _settingButtonImage.color = color;

        Color color2 = _howToPlayButtonImage.color;
        color2.a = 35f;
        _howToPlayButtonImage.color = color2;
    }

    public void HowToPlayHoverEnter()
    {
        // SETTINGS¸¸ »ìÂ¦ ¾îµÓ°Ô
        Color color = _settingButtonImage.color;
        color.a = 0.35f;
        _settingButtonImage.color = color;

        // HOW TO PLAY »ìÂ¦ ¾îµÓ°Ô
        Color color2 = _mainMenuButtonImage.color;
        color2.a = 0.35f;
        _mainMenuButtonImage.color = color2;
    }

    public void HowToPlayHoverExit()
    {
        Color color = _settingButtonImage.color;
        color.a = 35f;
        _settingButtonImage.color = color;

        Color color2 = _mainMenuButtonImage.color;
        color2.a = 35f;
        _mainMenuButtonImage.color = color2;
    }


}
