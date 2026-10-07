using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Check Point")]
    [SerializeField] private Transform _respawnPoint;

    private bool _isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            ActivateCheckPoint(other);
        }
    }

    private void ActivateCheckPoint(Collider other)
    {
        PlayerController2 playerController = other.GetComponent<PlayerController2>();

        if (playerController == null)
        {
            return;
        }

        playerController.SetRespawnPosition(_respawnPoint.position);

        _isActivated = true;

        Debug.Log("체크포인트 활성화");
    }
}
