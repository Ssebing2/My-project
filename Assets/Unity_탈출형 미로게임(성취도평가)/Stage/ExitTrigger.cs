using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameManager2 _gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ClearGame();
        }
    }

    private void ClearGame()
    {
        _gameManager.ClearGame();

        Debug.Log("플레이어가 출구에 도착했습니다.");
    }
}
