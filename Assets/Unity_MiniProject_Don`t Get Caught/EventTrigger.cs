using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Door _door;

    private bool _isTrigger;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null )
        {
            if (!_isTrigger)
            {
                Debug.Log("이벤트 발생!");

                _enemy.SetActive(true);
                _door.CloseDoor();
                _door.LockDoor();

                _isTrigger = true;
            }
        }
    }
}
