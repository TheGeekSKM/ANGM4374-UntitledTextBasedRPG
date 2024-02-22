using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory")]
    [SerializeField] Inventory _playerInventory;

    [Header("UI")]
    [SerializeField] GameObject _itemContentPanel;
    [SerializeField] GameObject _itemDisplayUsePrefab;
    [SerializeField] GameObject _itemDisplayThrowPrefab;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UseItems()
    {

        DisplayItems(true);
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GameInventoryState, .5f);
    }

    public void ThrowItems()
    {
        DisplayItems(false);
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GameInventoryState, .5f);

    }

    public void CloseInventory()
    {
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GameExtraActionState, .5f);
    }

    public void DisplayItems(bool use)
    {
        //clear
        foreach (Transform child in _itemContentPanel.transform)
        {
            Destroy(child.gameObject);
        }


        if (use)
        {
            foreach (var item in _playerInventory.Items)
            {
                var itemDisplay = Instantiate(_itemDisplayUsePrefab, _itemContentPanel.transform);
                itemDisplay.GetComponent<ItemDisplay>().SetItem(item);
            }
        }
        else
        {
            foreach (var item in _playerInventory.Items)
            {
                var itemDisplay = Instantiate(_itemDisplayThrowPrefab, _itemContentPanel.transform);
                itemDisplay.GetComponent<ItemDisplay>().SetItem(item);
            }
        }
    }





}
