using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Door _door;

    [Header("좀비 등장 사운드")]
    [SerializeField] private AudioSource _zombieVoiceAudioSource;
    [SerializeField] private AudioClip _zombieAppearClip;

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

                _zombieVoiceAudioSource.PlayOneShot(_zombieAppearClip);

                _isTrigger = true;
            }
        }
    }
}
