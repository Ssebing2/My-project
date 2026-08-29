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

    public void Interact()
    {
        Debug.Log("æ∆¿Ã≈€ »πµÊ!");


        switch (_itemType)
        {
            case EItemType.Key:
                _inventory.GetKey();
                break;
            case EItemType.Fuse:
                _inventory.GetFuse();
                break;
        }

        Destroy(gameObject);
    }
}
