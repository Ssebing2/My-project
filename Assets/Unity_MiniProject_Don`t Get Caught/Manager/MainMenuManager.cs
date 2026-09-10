using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI 사운드")]
    [SerializeField] private AudioSource _uiAudioSource;
    [SerializeField] private AudioClip _buttonHoverClip;
    [SerializeField] private AudioClip _buttonClickClip;

    [Header("페이드")]
    [SerializeField] private CanvasGroup _fadeCanvasGroup;
    [SerializeField] private float _fadeDuration = 1.0f;

    public void StartGame()
    {
        StartCoroutine(StartGameDelay());
    }

    private IEnumerator StartGameDelay()
    {
        Debug.Log("START 클릭 사운드 실행!");

        _uiAudioSource.PlayOneShot(_buttonClickClip);

        yield return new WaitForSecondsRealtime(0.2f);

        float time = 0f;

        while (time < _fadeDuration)
        {
            time += Time.unscaledDeltaTime;

            _fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, time / _fadeDuration);

            yield return null;
        }

        _fadeCanvasGroup.alpha = 1f;

        SceneManager.LoadScene("GameScene");
    }

    public void QultGame()
    {
        StartCoroutine(QuitGameDelay());

        Debug.Log("게임 종료");
    }

    private IEnumerator QuitGameDelay()
    {
        _uiAudioSource.PlayOneShot(_buttonClickClip);

        yield return new WaitForSecondsRealtime(0.2f);

        Application.Quit();
    }

    public void PlayButtonHoverSound()
    {
        Debug.Log("호버 사운드 실행!");
        _uiAudioSource.PlayOneShot(_buttonHoverClip);
    }

    public void PlayButtonClickSound()
    {
        Debug.Log("클릭 사운드 실행!");
        _uiAudioSource.PlayOneShot(_buttonClickClip);
    }
}
