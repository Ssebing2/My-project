using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorkingInstruction : MonoBehaviour
{
    [Header("성공 연출")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _cameraRotateSpeed = 30f;
    [SerializeField] private CameraFollowBasic _cameraFollow;
    [SerializeField] private GameObject _backToMainButton;
    [Header("사운드")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioClip _successSound;

    private float _survivalTime;        // 몇 초 동안 플레이했는지
    private int _coinScore;             // 현재 코인획득 스코어
    private bool _isSuccess;            // 최종 성공했는지
    private int _totalCoinCount;
    private int _collectedCoinCount;

    private void Start()
    {
        _backToMainButton.SetActive(false);
        _totalCoinCount = FindObjectsOfType<Coin>().Length;
    
    }

    private void Update()
    {
        if (!_isSuccess)
        {
            _survivalTime += Time.deltaTime;
        }

        if (!_isSuccess && FindObjectsOfType<Coin>().Length == 0)
        {
            _isSuccess = true;

            _cameraFollow.enabled = false;

            _backToMainButton.SetActive(true);
        }

        if (_isSuccess)
        {
            SuccessCamera();
            _audioSource.PlayOneShot(_successSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Coin coin = other.GetComponent<Coin>();

        if (coin == null)
        {
            return;
        }

        _coinScore += coin.Score;
        _audioSource.PlayOneShot(_coinSound);
        _collectedCoinCount++;

        Debug.Log($"코인 획득! +{coin.Score}");
        Debug.Log($"현재 코인 획득량 : {_coinScore}");

        Destroy(other.gameObject);
    }

    private void SuccessCamera()
    {
        _mainCamera.transform.RotateAround(transform.position, Vector3.up, _cameraRotateSpeed * Time.deltaTime);

        _mainCamera.transform.LookAt(transform);
    }

    private void OnGUI()
    {

        GUI.color = new Color(0f, 0f, 0f, 0.8f);
        GUI.Box(new Rect(10, 10, 200, 80), "");

        GUI.color = Color.white;
        GUI.Label(
           new Rect(20, 20, 300, 30),
           $"경과 시간 : {_survivalTime:F0}"
       );

        GUI.Label(
           new Rect(20, 40, 300, 30),
           $"코인 획득량 : {_collectedCoinCount}"
       );

        GUI.Label(
            new Rect(20, 60, 300, 30),
            $"총 코인 점수 : {_coinScore} point"
        );

        if (_isSuccess)
        {
            GUIStyle gameOverStyle = new GUIStyle();

            gameOverStyle.fontSize = 50;
            gameOverStyle.alignment = TextAnchor.MiddleCenter;
            gameOverStyle.normal.textColor = Color.yellow;

            GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 100, 500, 80), "SUCCESS!", gameOverStyle);

            GUIStyle scoreStyle = new GUIStyle();

            scoreStyle.fontSize = 35;
            scoreStyle.alignment = TextAnchor.MiddleCenter;
            scoreStyle.normal.textColor = Color.white;

            GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height / 2, 500, 60), $"Final Score : {_coinScore}", scoreStyle);
        }
    }

    public void BackToMainMenu()
    {
        Debug.Log("메인메뉴 버튼 클릭됨");

        SceneManager.LoadScene("MainMenu");
    }
}
