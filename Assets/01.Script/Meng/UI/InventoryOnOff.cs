using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOnOff : MonoBehaviour
{
    public InventoryUI inventoryUI;
    
    
    private bool _isInvenUp = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _isInvenUp = !_isInvenUp;
            inventoryUI.gameObject.SetActive(_isInvenUp);
            inventoryUI.SetCard(_isInvenUp);
        }
    }
}
