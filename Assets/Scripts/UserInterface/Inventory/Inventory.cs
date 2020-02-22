using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image inventorySize;
    public GridLayoutGroup inventory;
    public GameObject item;

    private int inventoryCount;
    private Vector2 inventoryScale;

    private void Start()
    {
        inventoryCount = 0;
        inventoryScale = inventorySize.rectTransform.sizeDelta;
    }

    private void Update()
    {
        // Testing adding 1 item
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (inventoryCount % 12 == 0 && inventoryCount != 0)
            {
                inventoryScale.y += 70;
                inventorySize.rectTransform.sizeDelta = inventoryScale;
            }
            GameObject newItem = Instantiate(item);
            newItem.transform.SetParent(inventory.transform, false);
            ++inventoryCount;
        }
    }
}
