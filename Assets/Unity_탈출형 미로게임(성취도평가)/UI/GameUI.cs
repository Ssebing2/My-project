using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameManager2 _gameManager;

    [Header("Game UI")]
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _stateText;

    [Header("Panel")]
    [SerializeField] private GameObject _readyPanel;
    [SerializeField] private GameObject _clearPanel;
    [SerializeField] private GameObject _gameOverPanel;

    private void Start()
    {
        UpdatePanel();
    }

    private void Update()
    {
        UpdateTimeText();
        UpdateStateText();
        UpdatePanel();
    }

    private void UpdateTimeText()
    {
        _timeText.text =
            $"TIME : {Mathf.CeilToInt(_gameManager.CurrentTime)}";
    }

    private void UpdateStateText()
    {
        _stateText.text =
            $"STATE : {_gameManager.GameState}";
    }

    private void UpdatePanel()
    {
        _readyPanel.SetActive( _gameManager.GameState == EGameState.Ready);

        _clearPanel.SetActive( _gameManager.GameState == EGameState.Clear);  
        
        _gameOverPanel.SetActive(_gameManager.GameState == EGameState.GameOver);
    }

    public void OnClickStart()
    {
        _gameManager.StartGame();
    }

    public void OnClickRestart()
    {
        _gameManager.RestartGame();
    }
}
