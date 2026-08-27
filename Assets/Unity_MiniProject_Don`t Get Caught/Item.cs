using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventory _inventory;

    public void Interact()
    {
        Debug.Log("æ∆¿Ã≈€ »πµÊ!");

        _inventory.GetKey();

        Destroy(gameObject);
     
    }

}
