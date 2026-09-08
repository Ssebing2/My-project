using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int _keyCount;
    [SerializeField] private int _fuseCount;

    [Header("인벤토리 UI")]
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private TMP_Text _keyCountText;
    [SerializeField] private TMP_Text _fuseCountText;

    private bool _isInventoryOpen;

    private void Start()
    {
        UpdateInventoryUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void GetKey()
    {
        _keyCount++;
        UpdateInventoryUI();
    }

    public bool HasKey()
    {
        return _keyCount > 0;
    }

    public void UseKey()
    {
        if (_keyCount > 0)
        {
            _keyCount--;
            UpdateInventoryUI();
        }
    }

    public void GetFuse()
    {
        _fuseCount++;
        UpdateInventoryUI();
    }

    public bool HasFuse()
    {
        return _fuseCount > 0;
    }

    public void UseFuse()
    {
        if (_fuseCount > 0)
        {
            _fuseCount--;
            UpdateInventoryUI();
        }
    }

    private void ToggleInventory()
    {
        _isInventoryOpen = !_isInventoryOpen;

        _inventoryPanel.SetActive(_isInventoryOpen);
    }

    private void UpdateInventoryUI()
    {
        _keyCountText.text = $"x {_keyCount}";
        _fuseCountText.text = $"x {_fuseCount}";
    }
}
