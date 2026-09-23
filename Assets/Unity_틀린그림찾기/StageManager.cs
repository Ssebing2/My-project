using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class StageManager : MonoBehaviour
{
    #region 인스펙터
    [Header("UI")]
    [SerializeField] private GameObject _findCountUI;
    [SerializeField] private TMP_Text _findCountText;
    [SerializeField] private float _maxTime = 30.0f;
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Image _incorrectMark;
    [SerializeField] private GameObject _timeOverPanel;
    [SerializeField] private GameObject _gameClearPanel;

    [Header("Stage")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _stage1Panel;
    [SerializeField] private GameObject _stage2Panel;
    [SerializeField] private Difference[] _stage1Differences;
    [SerializeField] private Difference[] _stage2Differences;

    [Header("패널티")]
    [SerializeField] private float _penaltyTime = 3.0f;

    [Header("추가시간")]
    [SerializeField] private float _bonusTime = 2.0f;

    [Header("Sound")]
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _correctSound;
    [SerializeField] private AudioClip _wrongSound;
    [SerializeField] private AudioClip _mainBGM;
    #endregion

    #region 변수
    private float _currentTime;
    private bool _isGameEnd = false;

    private int _findCount = 0;
    private int _maxFindCount = 5;
    private int _currentStage = 1;

    #endregion

    private void Start()
    {
        _findCountText.text = $"{_findCount} / {_maxFindCount}";

        _currentTime = _maxTime;

        _timeSlider.maxValue = _maxTime;
        _timeSlider.value = _currentTime;

        _audioSource.clip = _mainBGM;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    private void Update()
    {
        if (_isGameEnd)
        {
            return;
        }

        _currentTime -= Time.deltaTime;

        _timeSlider.value = _currentTime;

        if (_currentTime <= 0)
        {
            _currentTime = 0;

            _isGameEnd = true;

            _timeOverPanel.SetActive(true);
            CPrint.Log("TIME OVER");
        }
    }

    public void StartGame()
    {
        _mainPanel.SetActive(false);
        _stage1Panel.SetActive(true);
        _timeSlider.gameObject.SetActive(true);
        _findCountUI.gameObject.SetActive(true);
        _findCountText.gameObject.SetActive(true);

        _currentStage = 1;
        ResetStage();

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        _gameClearPanel.SetActive(false);

        _stage1Panel.SetActive(false);
        _stage2Panel.SetActive(false);

        _timeSlider.gameObject.SetActive(false);
        _findCountUI.gameObject.SetActive(false);
        _findCountText.gameObject.SetActive(false);

        _mainPanel.SetActive(true);

        _isGameEnd = true;
    }

    public void FindAnswer()
    {
        _findCount++;
        _audioSource.PlayOneShot(_correctSound);
        _findCountText.text = $"{_findCount} / {_maxFindCount}";
        _currentTime += _bonusTime;

        if (_currentTime >= _maxTime)
        {
            _currentTime = _maxTime;
        }

        if (_findCount == _maxFindCount)
        {
            Debug.Log("모든 틀린 그림을 찾았습니다.");
            NextStage();
        }
    }

    public void WrongClick()
    {
        _currentTime -= _penaltyTime;
        _audioSource.PlayOneShot(_wrongSound);

        if (_currentTime <= 0)
        {
            _currentTime = 0;
        }

        _incorrectMark.transform.position = Input.mousePosition;
        _incorrectMark.gameObject.SetActive(true);

        StartCoroutine(HideIncorrectMark());

    }

    private IEnumerator HideIncorrectMark()
    {
        yield return new WaitForSecondsRealtime(0.3f);

        _incorrectMark.gameObject.SetActive(false);
    }

    private void NextStage()
    {
        if (_currentStage == 1)
        {
            _stage1Panel.SetActive(false);
            _stage2Panel.SetActive(true);

            _currentStage = 2;

            ResetStage();
        }

        else if ( _currentStage == 2)
        {
            Debug.Log("게임 클리어");

            _isGameEnd = true;

            _gameClearPanel.SetActive(true);
        }

    }

    private void ResetStage()
    {
        _findCount = 0;
        _findCountText.text = $"{_findCount} / {_maxFindCount}";

        _currentTime = _maxTime;
        _timeSlider.value = _currentTime;

        for (int i = 0; i < _stage1Differences.Length; i++)
        {
            _stage1Differences[i].ResetDifference();
        }

        for (int i = 0; i < _stage2Differences.Length; i++)
        {
            _stage2Differences[i].ResetDifference();
        }

        _isGameEnd = false;
    }

    public void Retry()
    {
        _timeOverPanel.SetActive(false);
        _gameClearPanel.SetActive(false);
        ResetStage();
    }
}
