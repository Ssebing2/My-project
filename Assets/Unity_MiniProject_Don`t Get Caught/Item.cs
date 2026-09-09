using UnityEngine;

public enum EItemType
{
    Key,
    Fuse
}

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private EItemType _itemType;

    [Header("æ∆¿Ã≈€ »πµÊ ªÁøÓµÂ")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _keyPickupClip;
    [SerializeField] private AudioClip _fusePickupClip;

    public void Interact()
    {
        Debug.Log("æ∆¿Ã≈€ »πµÊ!");


        switch (_itemType)
        {
            case EItemType.Key:
                _inventory.GetKey();
                AudioSource.PlayClipAtPoint(_keyPickupClip, transform.position);
                break;
            case EItemType.Fuse:
                _inventory.GetFuse();
                AudioSource.PlayClipAtPoint(_fusePickupClip,transform.position);
                break;
        }

        Destroy(gameObject);
    }

    public string GetInteractionText()
    {
        switch (_itemType)
        {
            case EItemType.Key:
                return "[ E ] PICK UP KEY";

            case EItemType.Fuse:
                return "[ E ] PICK UP FUSE";
        }

        return "[ E ] ªÛ»£¿€øÎ";
    }
}
