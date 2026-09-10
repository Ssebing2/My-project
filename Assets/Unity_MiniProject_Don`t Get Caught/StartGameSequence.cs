using System.Collections;
using UnityEngine;

public class StartGameSequence : MonoBehaviour
{
    [Header("페이드")]
    [SerializeField] private CanvasGroup _fadeCanvasGroup;
    [SerializeField] private float _fadeDuration = 1.0f;

    [Header("HOW TO PLAY")]
    [SerializeField] private GameObject _howToPlayPanel;
    [SerializeField] private float _howToPlayDuration = 5.0f;

    [Header("손전등 안내")]
    [SerializeField] private GameObject _flashlightGuide;


    private void Start()
    {
        StartCoroutine(StartSequence());
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;

        while (time < _fadeDuration)
        {
            time += Time.unscaledDeltaTime;

            _fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, time /  _fadeDuration);

            yield return null;
        }

        _fadeCanvasGroup.alpha = 0f;
    }

    private IEnumerator StartSequence()
    {
        // 1. 게임 화면 Fade In
        yield return StartCoroutine(FadeIn());

        // 2. 잠깐 대기
        yield return new WaitForSecondsRealtime(0.5f);

        // 3. HOW TO PLAY 표시
        _howToPlayPanel.SetActive(true);

        // 4. 일정 시간 동안 보여주기
        yield return new WaitForSecondsRealtime(_howToPlayDuration);

        // 5. HOW TO PLAY 종료
        _howToPlayPanel.SetActive(false);

        yield return new WaitForSecondsRealtime(1.0f);

        _flashlightGuide.SetActive(true);
    }
}
