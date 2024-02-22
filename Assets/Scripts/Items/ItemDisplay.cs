using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] Button _button;
    [SerializeField] Inventory _playerInventory;

    protected ItemData _itemData;


    void OnValidate()
    {
        if (_button == null)
        {
            //search children for button
            _button = GetComponentInChildren<Button>();
        }
    }

    void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
    public void SetItem(ItemData item)
    {
        _itemData = item;
        _itemName.text = item.ItemName;
    }

    public virtual void OnButtonClick()
    {
        Debug.Log("Item clicked");
        if (_playerInventory.AddItem(_itemData))
        {
            Destroy(gameObject);
        }
    }
}
