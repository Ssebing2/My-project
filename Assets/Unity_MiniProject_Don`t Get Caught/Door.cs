using UnityEngine;
using Unity.AI.Navigation;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private NavMeshLink _navMeshLink;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private bool _isLocked;

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

        Vector3 isOpen = new Vector3(0f, 90f, 0f);
        Vector3 isClose = new Vector3(0f, -90f, 0f);

        if (_isLocked  && !_inventory.HasKey())
        {
            Debug.Log("문이 잠겨있다.");
            return;
        }
        
        if (!_isOpen)
        {
            transform.localRotation = _openRotation;
            _navMeshLink.enabled = true;
        }

        else
        {
            transform.localRotation = _closeRotation;
            _navMeshLink.enabled = false;
        }

        _isOpen = !_isOpen;
            
    }
}
