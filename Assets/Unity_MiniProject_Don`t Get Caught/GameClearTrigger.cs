using UnityEngine;

public class GameClearTrigger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    
    private bool _isTrigger;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            if (!_isTrigger && _gameManager.IsPowerRestored())
            {
                _gameManager.GameClear();
                Debug.Log("GAME CLEAR");
                _isTrigger = true;
            }
        }
    }
}
