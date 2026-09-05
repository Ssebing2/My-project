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
            _isOpen = true;
            return;
        }

        if (!_isFuseInstalled && _inventory.HasFuse())
        {
            _inventory.UseFuse();               // 소지 Fuse 소모
            _installedFuse.SetActive(true);     // 배전함에 Fuse 넣기
            _isFuseInstalled = true;            // Fuse 설치완료
            _door.UnlockDoor();                 // 방 문 잠금해제

            _gameManager.RestorePower();        // 전력 복구 상태 저장
        }       
    }
}
