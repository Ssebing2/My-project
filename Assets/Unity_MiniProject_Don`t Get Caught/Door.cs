using UnityEngine;
using Unity.AI.Navigation;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private NavMeshLink _navMeshLink;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private bool _isLocked;

    [Header("문 사운드")]
    [SerializeField] private AudioSource _doorAudioSource;
    [SerializeField] private AudioClip _openClip;
    [SerializeField] private AudioClip _closeClip;
    [SerializeField] private AudioClip _lockClip;

    private bool _isOpen;
    private Quaternion _closeRotation;
    private Quaternion _openRotation;

    private void Start()
    {
        _closeRotation = transform.localRotation;
        _openRotation = _closeRotation * Quaternion.Euler(0f, 90f, 0f);

        if (_navMeshLink == null)
        {
            Debug.LogError($"NavMeshLink 연결 안 된 문 : {gameObject.name}", gameObject);
            return;
        }

        _navMeshLink.enabled = false;
    }

    public void Interact()
    {
        Debug.Log("문과 상호작용 성공!");

        if (_isLocked  && !_inventory.HasKey())
        {
            Debug.Log("문이 잠겨있다.");

            _doorAudioSource.PlayOneShot(_lockClip);
            return;
        }

        if (_isLocked && _inventory.HasKey())
        {
            _inventory.UseKey();
            _isLocked = false;        
        }

        if (!_isOpen)
        {
            transform.localRotation = _openRotation;
            _navMeshLink.enabled = true;

            _doorAudioSource.PlayOneShot(_openClip);
        }

        else
        {
            transform.localRotation = _closeRotation;
            _navMeshLink.enabled = false;

            _doorAudioSource.PlayOneShot(_closeClip);
        }

        _isOpen = !_isOpen;         
    }

    public void CloseDoor()
    {
        if (_isOpen)
        {
            transform.localRotation = _closeRotation;
            _navMeshLink.enabled = false;

            _isOpen = false;
        }
    }

    public void LockDoor()
    {
        _isLocked = true;
    }

    public void UnlockDoor()
    {
        _isLocked = false;
    }
}
