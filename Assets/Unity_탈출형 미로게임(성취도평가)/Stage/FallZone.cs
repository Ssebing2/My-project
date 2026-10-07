using UnityEngine;

public class FallZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other);
        }
    }

    private void RespawnPlayer(Collider other)
    {
        PlayerController2 playerController = other.GetComponent<PlayerController2>();

        if (playerController == null)
        {
            return;
        }

        playerController.RespawnPlayer();

        Debug.Log("플레이어 낙하 - 체크포인트로 복귀");
    }
}
