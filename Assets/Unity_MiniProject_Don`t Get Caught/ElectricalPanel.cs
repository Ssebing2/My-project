using UnityEngine;

public class ElectricalPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private Transform _panelDoorPivot;
    [SerializeField] private GameObject _installedFuse;
    [SerializeField] private Door _door;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private bool _isOpen;
    [SerializeField] private bool _isFuseInstalled;

    [Header("퓨즈 장착 사운드")]
    [SerializeField] private AudioSource _fuseaudioSource;
    [SerializeField] private AudioClip _fuseInsertClip;

    [Header("배전함 커버 여는 사운드")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _opencoverClip;

    private Quaternion _closedRotation;
    private Quaternion _openedRotation;

    private void Start()
    {
        _closedRotation = _panelDoorPivot.localRotation;
        _openedRotation = _closedRotation * Quaternion.Euler(0f, 120f, 0f);
    }

    public void Interact()
    {

        Debug.Log("배전함과 상호작용 성공!");

        if (!_isOpen)
        {
            _panelDoorPivot.localRotation = _openedRotation;
            _audioSource.PlayOneShot(_opencoverClip);
            _isOpen = true;
            return;
        }

        if (!_isFuseInstalled && _inventory.HasFuse())
        {
            _inventory.UseFuse();               // 소지 Fuse 소모
            _installedFuse.SetActive(true);     // 배전함에 Fuse 넣기
            _isFuseInstalled = true;            // Fuse 설치완료
            _door.UnlockDoor();                 // 방 문 잠금해제

            _fuseaudioSource.PlayOneShot(_fuseInsertClip); // 퓨즈 장착음

            _gameManager.RestorePower();        // 전력 복구 상태 저장
        }       
    }

    public string GetInteractionText()
    {
        if (!_isOpen)
            return "[ E ] OPEN";

        if (!_isFuseInstalled && _inventory.HasFuse())
            return "[ E ] INSERT FUSE";

        return "";
    }
}
