using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private bool _isTrigger;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null && !_isTrigger)
        {
            _gameManager.GameOver();

            Debug.Log("GAME OVER");
            _isTrigger = true;
        }


    }
}
